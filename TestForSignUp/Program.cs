﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace TestForSignUp
{
    class Program
    {
        static void Main(string[] args)
        {
             var browser = new ChromeDriver();
             browser.Navigate().GoToUrl("https://localhost:44335/signup");
             Thread.Sleep(5000);
             var input = new InputGenerator();
             var filler = new ElementFiller(browser);
             filler.Fill(input.commonString, input.emailString, input.dateString);
             var buttons = browser.FindElementsByCssSelector("[type=\"button\"]");
             buttons[1].Click();
             Thread.Sleep(5000);
             var submitButton = browser.FindElementByCssSelector("[type=\"Submit\"]");
             submitButton.Click();           
        }
    }
}