using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading;

namespace TestForSignUp
{
    class ElementFiller
    {
        ReadOnlyCollection<IWebElement> inputs;
        ReadOnlyCollection<IWebElement> inputsWithoutDate;
        ReadOnlyCollection<IWebElement> emailInputs;
        
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
