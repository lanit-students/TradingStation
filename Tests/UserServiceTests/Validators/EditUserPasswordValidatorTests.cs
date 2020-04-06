using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Linq;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class EditUserPasswordValidatorTests
    {
        private IValidator<EditUserRequest> validator = new EditUserPasswordValidator();

        [Test]
        public void AllFieldsEmpty()
        {
            var user = new EditUserRequest();

            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Test]
        public void EmptyOldPassword()
        {
            var user = new EditUserRequest()
            {
                OldPassword = "",
                NewPassword = "password",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyNewPassword()
        {
            var user = new EditUserRequest()
            {
                OldPassword = "password",
                NewPassword = "",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullOldPassword()
        {
            var user = new EditUserRequest()
            {
                OldPassword = null,
                NewPassword = "password",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullNewPassword()
        {
            var user = new EditUserRequest()
            {
                OldPassword = "password",
                NewPassword = null,
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void OldAndNewPasswordsEqual()
        {
            var user = new EditUserRequest()
            {
                OldPassword = "password",
                NewPassword = "password",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.OldAndNewPasswordsEqual);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void ValidPasswordData()
        {
            var user = new EditUserRequest()
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            Assert.DoesNotThrow(() => validator.ValidateAndThrow(user));
        }
    }
}
