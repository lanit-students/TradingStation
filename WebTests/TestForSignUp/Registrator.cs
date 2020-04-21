using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;

namespace TestForSignUp
{
    internal static class Registrator
    {
        internal static bool RegistrateWithCorrectData(InputGenerator input)
        {
            try
            {
                var browser = new ChromeDriver();
                browser.Navigate().GoToUrl("https://localhost:44335/signup");
                Thread.Sleep(5000);
                var filler = new ElementFiller(browser);
                filler.Fill(input.nameAndLastName, input.email, input.date, input.password);
                var buttons = browser.FindElementsByCssSelector("[type=\"button\"]");
                var nextButton = buttons[1];
                nextButton.Click();
                Thread.Sleep(5000);
                var path = Path.GetFullPath(@"..\..\..\testPhoto.jpg").ToString();
                ImageAdd(browser, path);
                Thread.Sleep(5000);
                var submitButton = browser.FindElementByCssSelector("[type=\"Submit\"]");
                submitButton.Click();
                Thread.Sleep(5000);
                var errors = browser.FindElementsByCssSelector("[style=\" color: red;\"]");
                return errors.Count != 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool RegistrateWithIncorrectData(InputGenerator input)
        {
            var browser = new ChromeDriver();
            browser.Navigate().GoToUrl("https://localhost:44335/signup");
            Thread.Sleep(5000);
            var filler = new ElementFiller(browser);
            filler.Fill(input.nameAndLastName, input.email, input.date, input.password);
            var buttons = browser.FindElementsByCssSelector("[type=\"button\"]");
            var nextButton = buttons[1];
            nextButton.Click();
            Thread.Sleep(5000);
            var errors = browser.FindElementsByCssSelector("[class=\"validation-message\"]");
            return errors.Count == 4;
        }

        private static void ImageAdd(ChromeDriver browser, string path)
        {
            var imageButton = browser.FindElementByCssSelector("[type=\"file\"]");
            imageButton.SendKeys(path);
        }
    }
}
