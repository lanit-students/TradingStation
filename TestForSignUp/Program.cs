using OpenQA.Selenium;

namespace TestForSignUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Navigate().GoToUrl("");

        }
    }
}
