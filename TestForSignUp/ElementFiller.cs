using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace TestForSignUp
{
    internal class ElementFiller
    {
        private ReadOnlyCollection<IWebElement> inputs;
        private ReadOnlyCollection<IWebElement> inputsWithoutDate;
        private ReadOnlyCollection<IWebElement> emailInputs;
        
        internal ElementFiller(ChromeDriver browser)
        {
            inputs = browser.FindElementsByXPath("//input");
            inputsWithoutDate = browser.FindElementsByCssSelector("[placeholder]");
            emailInputs = browser.FindElementsByCssSelector("[placeholder*=\"@\"]");
        }

        internal void Fill(string common, string email, string date)
        {
            foreach (var inputField in inputs)
            {
                if (emailInputs.Contains(inputField))
                {
                    inputField.SendKeys(email + Keys.Enter);
                    continue;
                }
                if (!(inputsWithoutDate.Contains(inputField)))
                {
                    inputField.SendKeys(date + Keys.Enter);
                    continue;
                }
                inputField.SendKeys(common + Keys.Enter);
            }
        }       
    }
}
