using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Linq;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class EditUserInfoValidatorTests
    {
        private IValidator<EditUserRequest> validator = new EditUserInfoValidator();

        [Test]
        public void AllFieldsEmpty()
        {
            var user = new EditUserRequest();

            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Test]
        public void NullFirstName()
        {
            var user = new EditUserRequest
            {
                FirstName = null,
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyFirstName()
        {
            var user = new EditUserRequest
            {
                FirstName = "",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameContainsNumbers()
        {
            var user = new EditUserRequest
            {
                FirstName = "Leo12",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameError);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameFirstLetterIsNotUpperCase()
        {
            var user = new EditUserRequest
            {
                FirstName = "leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameError);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameRestLettersAreNotLowerCase()
        {
            var user = new EditUserRequest
            {
                FirstName = "LeO",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameError);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void WrongBirthday()
        {
            var user = new EditUserRequest
            {
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Now.AddDays(1)
            };
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdayYoung);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissBirthday()
        {
            var user = new EditUserRequest
            {
                FirstName = "Leo",
                LastName = "Kor",
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdayEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissFirstName()
        {
            var user = new EditUserRequest
            {
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissLastName()
        {
            var user = new EditUserRequest
            {
                FirstName = "Leo",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameTooLong()
        {
            var user = new EditUserRequest
            {
                FirstName = "C" + new string('c', 40),
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameTooLong);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameConsistOnlyOneLetter()
        {
            var user = new EditUserRequest
            {
                FirstName = "L",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameTooShort);

            Assert.IsNotNull(expectedError);

        }

        [Test]
        public void ValidData()
        {
            var user = new EditUserRequest
            {
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            Assert.DoesNotThrow(() => validator.ValidateAndThrow(user));
        }

    }
}
