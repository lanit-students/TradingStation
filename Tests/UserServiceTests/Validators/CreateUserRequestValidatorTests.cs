using System;
using UserService.Validators;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using DTO.RestRequests;

namespace UserServiceTests.Validators
{
    public class CreateUserRequestValidatorTests
    {
        private IValidator<CreateUserRequest> validator = new CreateUserRequestValidator();

        [Test]
        public void AllFieldsEmpty()
        {
            var user = new CreateUserRequest();

            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Test]
        public void NullEmail()
        {
            var user = new CreateUserRequest
            {
                Email = null,
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyEmail()
        {
            var user = new CreateUserRequest {
                Email = "",
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void InvalidEmail()
        {
            var user = new CreateUserRequest
            {
                Email = "email.yandex.ru",
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.InvalidEmail);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyPassword()
        {
            var user = new CreateUserRequest {
                Email = "email@yandex.ru",
                Password = "",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullPassword()
        {
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = null,
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullFirstName()
        {
            var user = new CreateUserRequest {
                Email = "email@yandex.ru",
                Password = "123",
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
            var user = new CreateUserRequest {
                FirstName = "",
                Email = "email@yandex.ru",
                Password = "123",
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
        public void FirstNameContainsNumbers()
        {
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "Leo12",
                LastName = "Kor",
                Birthday = DateTime.Today
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
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "leo",
                LastName = "Kor",
                Birthday = DateTime.Today
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
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "LeO",
                LastName = "Kor",
                Birthday = DateTime.Today
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
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
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
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor"
            };
            
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdayEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissEmail()
        {
            var user = new CreateUserRequest
            {
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissPassword()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissFirstName()
        {
            var user = new CreateUserRequest
            {
                Password = "123",
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
        public void MissLastName()
        {
            var user = new CreateUserRequest
            {
                Password = "123",
                FirstName = "Leo",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmailTooLong()
        {
            var user = new CreateUserRequest
            {
                Email = new string('c', 50) + "@ya.ru",
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailTooLong);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameTooLong()
        {
            var user = new CreateUserRequest
            {
                Password = "123",
                FirstName = "C" + new string('c', 40),
                LastName = "Kor",
                Birthday = DateTime.Today
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
            var user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "C",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-18)
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
            var user = new CreateUserRequest { 
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
                };
            
            Assert.DoesNotThrow(() => validator.ValidateAndThrow(user));
        }
    }
}