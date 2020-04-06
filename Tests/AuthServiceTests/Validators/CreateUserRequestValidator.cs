using DTO.RestRequests;
using NUnit.Framework;
using System;
using UserService.Validators;
using static UserService.Validators.ErrorsMessages;


namespace AuthenticationServiceTests.Validators
{
    class CreateUserRequestValidatorTests
    {
        CreateUserRequestValidator validator;
        CreateUserRequest correctData;
        CreateUserRequest incorrectBirthday;
        CreateUserRequest incorrectEmail;
        CreateUserRequest incorrectFirstName;
        CreateUserRequest incorrectLastName;
        CreateUserRequest emptyPassword;

        [SetUp]
        public void Initialize()
        {
            validator = new CreateUserRequestValidator();
            correctData = new CreateUserRequest
            {
                Birthday = new DateTime(2000, 01, 01),
                Email = "myfirstemail@gmail.com",
                FirstName = "Steve",
                LastName = "Panin",
                Password = "myPassword"
            };

            incorrectBirthday = new CreateUserRequest
            {
                Birthday = new DateTime(1000, 01, 01),
                Email = "bla@bla.com",
                FirstName = "Steve",
                LastName = "Panin",
                Password = "myPassword"
            };

            incorrectEmail = new CreateUserRequest
            {
                Birthday = new DateTime(2000, 01, 01),
                Email = "testFakeEmail.com",
                FirstName = "Steve",
                LastName = "Panin",
                Password = "myPassword"
            };

            incorrectFirstName = new CreateUserRequest
            {
                Birthday = new DateTime(2000, 01, 01),
                Email = "bla@bla.com",
                FirstName = "Steve1100",
                LastName = "Panin",
                Password = "myPassword"
            };

            incorrectLastName = new CreateUserRequest
            {
                Birthday = new DateTime(2000, 01, 01),
                Email = "bla@bla.com",
                FirstName = "Steve",
                LastName = "",
                Password = "myPassword"
            };

            emptyPassword = new CreateUserRequest
            {
                Birthday = new DateTime(2000, 01, 01),
                Email = "bla@bla.com",
                FirstName = "Steve",
                LastName = "Panin",
                Password = ""
            };
        }

        [Test]
        public void CorrectDataTest()
        {
            Assert.IsTrue(validator.Validate(correctData).IsValid);
        }

        [Test]
        public void IncorrectPasswordNameTest()
        {
            var validationResult = validator.Validate(emptyPassword);
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, EmptyPassword);
        }

        [Test]
        public void IncorrectFirstNameTest()
        {
            var validationResult = validator.Validate(incorrectFirstName);
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, FirstNameError);
        }

        [Test]
        public void IncorrecLastNameTest()
        {
            var validationResult = validator.Validate(incorrectLastName);
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, LastNameEmpty);
        }

        [Test]
        public void IncorrectBirthdayTest()
        {
            var validationResult = validator.Validate(incorrectBirthday);
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, BirthdayYoung);
        }

        [Test]
        public void IncorrectEmailTest()
        {
            var validationResult = validator.Validate(incorrectEmail);
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, InvalidEmail);
        }
    }
}
