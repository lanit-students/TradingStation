using GUI.ViewModels;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SingUpModelValidationTests
{
    public class Tests
    {
        SignUpViewModel correctModel;
        SignUpViewModel empityNameModel;
        SignUpViewModel incorrectEmailModel;
        SignUpViewModel incorrectPasswordModel;
        RequiredAttribute requiredChecker;
        EmailAddressAttribute emailChecker;
        CompareAttribute comparer;

        [SetUp]
        public void Setup()
        {
            correctModel = new SignUpViewModel { FirstName = "first", LastName = "last", Birthday = new System.DateTime(2001, 04, 05), Password = "12345", ConfirmPassword = "12345", Email = "test@mail.ru" };
            empityNameModel = new SignUpViewModel { Birthday = new System.DateTime(2001, 04, 05), Password = "12345", ConfirmPassword = "12345", Email = "test@mail.ru" };
            incorrectEmailModel = new SignUpViewModel { FirstName = "first", LastName = "last", Birthday = new System.DateTime(2001, 04, 05), Password = "12345", ConfirmPassword = "12345", Email = "incorrectEMail.com" };
            incorrectPasswordModel = new SignUpViewModel { FirstName = "first", LastName = "last", Birthday = new System.DateTime(2001, 04, 05), Password = "12345", ConfirmPassword = "54321", Email = "test@mail.ru" };
            requiredChecker = new RequiredAttribute();
            emailChecker = new EmailAddressAttribute();
            comparer = new CompareAttribute("12345");
        }

        [Test]
        public void CorrectModelTest()
        {
            var props = correctModel.GetType().GetProperties();
            foreach (var item in props)
            {
                Assert.AreEqual(props.Length, 6);
                Assert.IsTrue(requiredChecker.IsValid(item.GetValue(correctModel)));
            }
        }

        [Test]
        public void EmpityNameTest()
        {
            var props = empityNameModel.GetType().GetProperties();
            foreach (var item in props)
            {
                Assert.AreEqual(props.Length, 6);
                if (item.Name == "FirstName" || item.Name == "LastName")
                    Assert.IsFalse(requiredChecker.IsValid(item.GetValue(empityNameModel)));
                else
                    Assert.IsTrue(requiredChecker.IsValid(item.GetValue(empityNameModel)));
            }
        }

        [Test]
        public void IncorrecEmailTest()
        {
            var incorrecEmail = incorrectEmailModel.GetType().GetProperty("Email").GetValue(incorrectEmailModel);
            var correcEmail = correctModel.GetType().GetProperty("Email").GetValue(correctModel);
            Assert.AreEqual(correcEmail, "test@mail.ru");
            Assert.IsTrue(emailChecker.IsValid(correcEmail));
            Assert.IsFalse(emailChecker.IsValid(incorrecEmail));
        }

        [Test]
        public void IncorrectPasswordTest()
        {
            var confirmPasswordMember = incorrectPasswordModel.GetType().
                GetMember("ConfirmPassword");
            foreach (MemberInfo k in confirmPasswordMember)
            {
                var d = k.GetCustomAttributes(true);
                Assert.AreEqual(d.Length, 1);
                Assert.IsTrue(d[0] is CompareAttribute);
                var od = (CompareAttribute)d[0];
                Assert.IsTrue(od.IsValid(correctModel));
            }
            Assert.AreNotEqual(confirmPasswordMember, null);
        }
    }
}