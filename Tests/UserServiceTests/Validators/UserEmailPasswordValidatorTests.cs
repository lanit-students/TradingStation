﻿using System;
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest {
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
        public void WrongEmail()
        {
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest {
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest {
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
            CreateUserRequest user = new CreateUserRequest {
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
        public void FirstNameRestLettersIsNotLowerCase()
        {
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
                    error => error.ErrorMessage == ErrorsMessages.FutureErrorBirthday);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissBirthday()
        {
            CreateUserRequest user = new CreateUserRequest
            {
                Email = "email@yandex.ru",
                Password = "123",
                FirstName = "Leo",
                LastName = "Kor"
            };
            
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdatEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissEmail()
        {
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
            CreateUserRequest user = new CreateUserRequest
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
        public void BirthdayYoung()
        {
            CreateUserRequest user = new CreateUserRequest
            {
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-17)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdayYoung);

            Assert.IsNotNull(expectedError);
        }


        [Test]
        public void Ok()
        {
            CreateUserRequest user = new CreateUserRequest { 
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