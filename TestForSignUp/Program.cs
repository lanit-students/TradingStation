using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestForSignUp
{
    class Program
    {
        static void Main()
        {
            try
            {
                var browser = new ChromeDriver();
                browser.Navigate().GoToUrl("https://localhost:44335/signup");
                Thread.Sleep(5000);
                var input = new InputGenerator();
                var filler = new ElementFiller(browser);
                filler.Fill(input.commonString, input.emailString, input.dateString);
                var buttons = browser.FindElementsByCssSelector("[type=\"button\"]");
                buttons[1].Click();
                Thread.Sleep(5000);
                var submitButton = browser.FindElementByCssSelector("[type=\"Submit\"]");
                submitButton.Click();
            }
            catch(Exception)
            {
                Console.WriteLine("Exception was detected");
            }
            Console.WriteLine("Successful");
        }
    }
}
