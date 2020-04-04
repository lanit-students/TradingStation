using System;
using UserService.Validators;
using DTO;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using DTO.RestRequests;

namespace UserServiceTests.Validators
{
    public class UserEmailPasswordValidatorTests
    {
        private IValidator<CreateUserRequest> validator = new CreateUserRequestValidator();

        [Test]
        public void AllFieldsEmpty()
        {
            CreateUserRequest user = new CreateUserRequest();

            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Test]
        public void NullEmail()
        {
            CreateUserRequest user = new CreateUserRequest { Email = null };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "Email must be.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyEmail()
        {
            CreateUserRequest user = new CreateUserRequest {Email = ""};

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "Email address must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void WrongEmail()
        {
            CreateUserRequest user = new CreateUserRequest { Email = "wrong.mail" };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "Email address must be in valid format.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyPassword()
        {
            CreateUserRequest user = new CreateUserRequest { Password = "" };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "Password must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullPassword()
        {
            CreateUserRequest user = new CreateUserRequest { Password = null };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "Password must be.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullFirstName()
        {
            CreateUserRequest user = new CreateUserRequest { FirstName = null };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "First name must be.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyFirstName()
        {
            CreateUserRequest user = new CreateUserRequest { FirstName = "" };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "First name must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameContainsNumbers()
        {
            CreateUserRequest user = new CreateUserRequest { FirstName = "Vadik12" };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "First name must not contain numbers and etc.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameFirstLetterIsNotUpperCase()
        {
            CreateUserRequest user = new CreateUserRequest { FirstName = "vadik" };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "The first letter must be uppercase and the rest must be lowercase");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameRestLettersIsNotLowerCase()
        {
            CreateUserRequest user = new CreateUserRequest { FirstName = "VaDik" };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "The first letter must be uppercase and the rest must be lowercase");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void WrongBirthday()
        {
            CreateUserRequest user = new CreateUserRequest { Birthday = DateTime.Today.AddDays(1) };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "You cannot be born in the future.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissBirthday()
        {
            CreateUserRequest user = new CreateUserRequest { };
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == "Birthday must not be empty.");

            Assert.IsNotNull(expectedError);
        }


    }
}