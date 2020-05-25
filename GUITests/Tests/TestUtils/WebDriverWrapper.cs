using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUITests.Tests.TestUtils
{
    public static class WebDriverWrapper
    {
        public static IWebElement FindElementByClass(ChromeDriver driver, string cssAttr, string text = null)
        {
            if (text != null)
            {
                return driver.FindElementByXPath($"//{cssAttr}[text()='{text}']");
            }

            return driver.FindElementByXPath($"//{cssAttr}");
        }

        public static ReadOnlyCollection<IWebElement> FindElementsBySelector(ChromeDriver driver, string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector));
        }

        public static ReadOnlyCollection<IWebElement> FindValidationErrors(ChromeDriver driver)
        {
            return driver.FindElementsByCssSelector("[class=\"validation-message\"]");
        }

        public static void FillForm(ChromeDriver driver, List<string> fields)
        {
            var inputs = FindElementsBySelector(driver, "input");

            for (var i = 0; i < fields.Count; ++i)
            {
                inputs[i].SendKeys(fields[i]);
            }
        }

        public static void UploadImage(ChromeDriver driver, string path)
        {
            driver.FindElementByCssSelector("[type=\"file\"]").SendKeys(path);
        }

        public static void SubmitForm(ChromeDriver driver, List<string> fields, string submitText = null)
        {
            FillForm(driver, fields);

            if (submitText == null)
            {
                driver.FindElement(By.ClassName("button")).Click();
            }
            else
            {
                FindElementByClass(driver, "button", submitText).Click();
            }
        }
    }
}
