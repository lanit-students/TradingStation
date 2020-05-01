using GUITestsEngine.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GUITestsEngine.Core
{
    public static class Engine
    {
        private static ConcurrentDictionary<Type, MethodSet> methodsToTest = new ConcurrentDictionary<Type, MethodSet>();

        private static ConcurrentDictionary<Type, ConcurrentBag<TestInfo>> testResults = new ConcurrentDictionary<Type, ConcurrentBag<TestInfo>>();

        private static IEnumerable<Type> GetClasses()
        {
            return Assembly.GetExecutingAssembly().ExportedTypes.Where(t => t.IsClass);
        }

        private static void QueueClassTests(Type type)
        {
            var methodSet = new MethodSet(type);

            if (methodSet.TestsCount > 0)
            {
                methodsToTest.TryAdd(type, methodSet);
            }
        }

        private static void RunCollectedTests()
        {
            Parallel.ForEach(methodsToTest.Keys, type =>
            {
                testResults.TryAdd(type, new ConcurrentBag<TestInfo>());

                foreach (var testMethod in methodsToTest[type].TestMethods)
                {
                    RunTestMethod(type, testMethod);
                }
            });
        }

        private static void RunTestMethod(Type type, MethodInfo method)
        {
            var ctor = type.GetConstructor(Type.EmptyTypes);

            if (ctor == null)
            {
                throw new FormatException($"{type.Name} must have parameterless constructor");
            }

            var instance = ctor.Invoke(null);

            var isPassed = false;

            try
            {
                method.Invoke(instance, null);
                isPassed = true;
            }
            catch { }
            finally
            {
                testResults[type].Add(new TestInfo(method.Name, isPassed));
            }
        }

        private static void PrintReport()
        {
            Console.WriteLine("Testing report:");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Found classes to test: {methodsToTest.Keys.Count}");

            var allMethodsCount = 0;

            foreach (var testedClass in methodsToTest.Keys)
            {
                allMethodsCount += methodsToTest[testedClass].TestsCount;
            }

            Console.WriteLine($"Found methods to test (total): {allMethodsCount}");

            foreach (var someClass in testResults.Keys)
            {
                if (testResults[someClass].Count != 0)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine($"Class: {someClass}");

                    var test = testResults;

                    foreach (var testInfo in testResults[someClass])
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"Tested method: {testInfo.MethodName}()");

                        if (testInfo.IsPassed)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Passed {testInfo.MethodName}() test");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Failed {testInfo.MethodName}() test");
                            Console.ResetColor();
                        }
                    }
                }
            }

            Console.WriteLine("-----------------------------");
        }

        public static void RunTests()
        {
            Parallel.ForEach(GetClasses(), someClass =>
            {
                QueueClassTests(someClass);
            });

            RunCollectedTests();

            Console.Clear();

            PrintReport();
        }
    }
}
