using GUITests.Tests.TestUtils;
using GUITestsEngine.Utils.Attributes;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GUITestsEngine.Tests
{
    public class SignUpTests
    {
        private readonly string imgPath = Path.GetFullPath(@"..\..\..\Resources\testPhoto.jpg").ToString();

        private List<string> fields = new List<string>()
        {
            "Name",
            "Surname",
            "01.01.2000",
            $"{Guid.NewGuid()}@gmail.com",
            "password",
            "password"
        };

        private void FillSignUpForm(ChromeDriver driver, List<string> input)
        {
            driver.Navigate().GoToUrl("https://localhost:44335/");
            Thread.Sleep(5000);

            WebDriverWrapper.FindElementByClass(driver, "a", "Sign up").Click();

            WebDriverWrapper.FillForm(driver, input);

            WebDriverWrapper.FindElementByClass(driver, "button", "Next").Click();

            Thread.Sleep(5000);
        }

        [Test]
        public void ValidSignUpTest()
        {
            var driver = new ChromeDriver();

            FillSignUpForm(driver, fields);

            Thread.Sleep(5000);

            WebDriverWrapper.UploadImage(driver, imgPath);

            Thread.Sleep(5000);

            WebDriverWrapper.FindElementByClass(driver, "button", "Submit").Click();

            Thread.Sleep(5000);

            var input = new List<string>()
            {
                Path.GetFullPath(@"..\..\..\Resources\testPhoto.jpg").ToString(),
                fields[3],
                fields[4]
            };

            WebDriverWrapper.SubmitForm(driver, input);

            Thread.Sleep(5000);

            var testResult = driver.Url == "https://localhost:44335/userinfo";
            driver.Close();

            if (!testResult)
            {
                throw new Exception();
            }
        }

        [Test]
        public void EmptySignUpTest()
        {
            var driver = new ChromeDriver();

            FillSignUpForm(driver, new List<string>());
            WebDriverWrapper.FindElementByClass(driver, "button", "Next").Click();

            var errors = WebDriverWrapper.FindValidationErrors(driver);

            Thread.Sleep(5000);

            driver.Close();

            if (errors.Count != 4)
            {
                throw new Exception();
            }
        }
    }
}
