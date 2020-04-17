using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace TestForSignUp
{
    public class Program
    {
        public static void Main()
        {
            var input = new InputGenerator();
            if (!Registrator.RegistrateWithCorrectData(input))
            {
                Console.WriteLine("Can't registrate (Exception)");
                return;
            }
            Thread.Sleep(5000);
            if (Registrator.RegistrateWithCorrectData(input))
            {
                Console.WriteLine("Can register with an existing email (Exception)");
                return;
            }
            Thread.Sleep(5000);
            if (!Registrator.RegistrateWithIncorrectData())
            {
                Console.WriteLine("Can register with incorrect data (Exception)");
                return;
            }
            Console.WriteLine("Successful");
        }
    }
}
