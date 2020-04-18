using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace TestForSignUp
{
    public class ElementFiller
    {
        private ReadOnlyCollection<IWebElement> inputs;
        private ReadOnlyCollection<IWebElement> inputsWithoutDate;
        private ReadOnlyCollection<IWebElement> emailInputs;
        private ReadOnlyCollection<IWebElement> passwordInputs;

        public ElementFiller(ChromeDriver browser)
        {
            inputs = browser.FindElementsByXPath("//input");
            inputsWithoutDate = browser.FindElementsByCssSelector("[placeholder]");
            emailInputs = browser.FindElementsByCssSelector("[placeholder*=\"@\"]");
            passwordInputs = browser.FindElementsByCssSelector("[type=\"password\"]");
        }

        public void Fill(string nameAndLastName, string email, string date, string password)
        {
            foreach (var inputField in inputs)
            {
                if (emailInputs.Contains(inputField))
                {
                    inputField.SendKeys(email + Keys.Enter);
                    continue;
                }
                if (passwordInputs.Contains(inputField))
                {
                    inputField.SendKeys(password + Keys.Enter);
                    continue;
                }
                if (!(inputsWithoutDate.Contains(inputField)))
                {
                    inputField.SendKeys(date + Keys.Enter);
                    continue;
                }
                inputField.SendKeys(nameAndLastName + Keys.Enter);
            }
        }       
    }
}
