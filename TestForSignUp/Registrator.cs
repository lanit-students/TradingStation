using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestForSignUp
{
    internal static class Registrator
    {
        internal static bool registrateWithCorrectData(InputGenerator input)
        {
            try
            {
                var browser = new ChromeDriver();
                browser.Navigate().GoToUrl("https://localhost:44335/signup");
                Thread.Sleep(5000);
                var filler = new ElementFiller(browser);
                filler.Fill(input.commonString, input.emailString, input.dateString);
                var buttons = browser.FindElementsByCssSelector("[type=\"button\"]");
                buttons[1].Click();
                Thread.Sleep(5000);
                var submitButton = browser.FindElementByCssSelector("[type=\"Submit\"]");
                submitButton.Click();
                Thread.Sleep(5000);
                var errors = browser.FindElementsByCssSelector("[style=\" color: red;\"]");
                if (errors.Count == 1) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        internal static bool registrateWithIncorrectData()
        {
            var browser = new ChromeDriver();
            browser.Navigate().GoToUrl("https://localhost:44335/signup");
            Thread.Sleep(5000);
            var filler = new ElementFiller(browser);
            filler.Fill("A", "A", "1");
            var buttons = browser.FindElementsByCssSelector("[type=\"button\"]");
            buttons[1].Click();
            Thread.Sleep(5000);
            var errors = browser.FindElementsByCssSelector("[class=\"validation-message\"]");
            if (errors.Count == 3) return true;
            return false;
        }
    }
}
