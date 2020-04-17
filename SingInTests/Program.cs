using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SingInTests
{
    internal class SignInTest
    {
        private CreateUserRequest request = new CreateUserRequest
        {
            FirstName = "Test",
            LastName = "Test",
            Birthday = DateTime.Today.AddYears(-20),
            Email = "testUser@mail.ru",
            Password = "password"
        };

        private static void Main(string[] args)
        {
            var test = new SignInTest();
            try
            {
                test.TryToSingUp();
            }
            catch (Exception e)
            {
                Console.WriteLine("User was created");
            }

            if (test.TryToSingIn())
            {
                Console.WriteLine("Successful");
            }
            else
            {
                Console.WriteLine("Something was wrong");
            }
        }

        private async Task TryToSingUp()
        {

            const string url = "https://localhost:5011/users/create";

            var client = new RestClient<CreateUserRequest, bool>(url, RestRequestType.POST);

            await client.Execute(request);
        }

        private bool TryToSingIn()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44335/signin");
            Thread.Sleep(5000);
            ReadOnlyCollection<IWebElement> inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(request.Email);
            inputs[1].SendKeys(request.Password);
            var submitButton = driver.FindElement(By.ClassName("button"));
            submitButton.Click();
            Thread.Sleep(5000);
            var ans = driver.Url != "https://localhost:44335/signin";
            driver.Close();
            return ans;
        }
    }
}