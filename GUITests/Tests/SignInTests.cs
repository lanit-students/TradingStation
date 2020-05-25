using DTO.RestRequests;
using GUITests.Tests.TestUtils;
using GUITestsEngine.Utils.Attributes;
using Kernel;
using Kernel.Enums;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GUITestsEngine.Tests
{
    public class SignInTests
    {
        private CreateUserRequest request = new CreateUserRequest
        {
            Email = "den89181827071@mail.ru",
            Password = "12345"
        };

        [Test]
        public void ValidSignInTest()
        {

            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://localhost:8080/");

            Thread.Sleep(5000);

            WebDriverWrapper.FindElementByClass(driver, "button", "Sign In").Click();

            Thread.Sleep(5000);

            var fields = new List<string>() { request.Email, request.Password };
            WebDriverWrapper.SubmitForm(driver, fields);

            Thread.Sleep(5000);
            
            var testResult = driver.Url == "http://localhost:8080/";

            driver.Close();

            if (!testResult)
            {
                throw new Exception();
            }
        }

        [Test]
        public void InvalidSignInTest()
        {
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://localhost:8080/");

            Thread.Sleep(5000);

            WebDriverWrapper.FindElementByClass(driver, "button", "Sign In").Click();

            Thread.Sleep(5000);

            var fields = new List<string>() { "", "" };
            WebDriverWrapper.SubmitForm(driver, fields);

            Thread.Sleep(5000);

            var errors = WebDriverWrapper.FindValidationErrors(driver);

            driver.Close();

            if (errors.Count != 2)
            {
                throw new Exception();
            }
        }
    }
}
