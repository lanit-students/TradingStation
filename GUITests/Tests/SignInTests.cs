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
            FirstName = "Test",
            LastName = "Test",
            Birthday = DateTime.Today.AddYears(-20),
            Email = $"{Guid.NewGuid()}@gmail.com",
            Password = "password"
        };

        [Test]
        public void SignInTest()
        {
            const string url = "https://localhost:5011/users/create";

            var client = new RestClient<CreateUserRequest, bool>(url, RestRequestType.POST);

            var commandResult = client.Execute(request);

            if (!commandResult)
            {
                throw new Exception();
            }

            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44335/");
            Thread.Sleep(5000);

            WebDriverWrapper.FindElementByClass(driver, "a", "Sign In").Click();

            var fields = new List<string>() { request.Email, request.Password };

            WebDriverWrapper.SubmitForm(driver,fields);

            Thread.Sleep(5000);
            var testResult = driver.Url == "https://localhost:44335/userinfo";
            driver.Close();

            if (!testResult)
            {
                throw new Exception();
            }
        }
    }
}
