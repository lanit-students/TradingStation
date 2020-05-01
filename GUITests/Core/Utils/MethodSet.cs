using GUITestsEngine.Utils.Attributes;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;

namespace GUITestsEngine.Utils
{
    public class MethodSet
    {
        public ConcurrentQueue<MethodInfo> TestMethods;

        public int TestsCount
                => TestMethods.Count;

        public MethodSet(Type type)
        {
            TestMethods = new ConcurrentQueue<MethodInfo>();

            QueueTestedClassMethods(type);
        }

        private bool IsMethodValid(MethodInfo method)
        {
            return method.GetParameters().Length == 0 && method.ReturnType == typeof(void);
        }

        public void QueueTestedClassMethods(Type type)
        {
            Parallel.ForEach(type.GetMethods(), method =>
            {
                if (method.GetCustomAttribute<TestAttribute>() != null)
                {
                    TryToQueueMethod(method, TestMethods);
                }
            });
        }

        private void TryToQueueMethod(MethodInfo method, ConcurrentQueue<MethodInfo> queue)
        {
            if (!IsMethodValid(method))
            {
                throw new FormatException("Method shouldn't return value or get parameters");
            }

            queue.Enqueue(method);
        }
    }
}
