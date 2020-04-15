using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SingInTests
{
    internal class SignInTest
    {
        private static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://localhost:44335/signin";

            Console.WriteLine("Hello World!");
        }
    }
}