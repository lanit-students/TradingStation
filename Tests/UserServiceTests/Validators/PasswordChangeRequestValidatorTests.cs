using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class PasswordChangeRequestValidatorTests
    {
        private IValidator<PasswordChangeRequest> validator = new PasswordChangeRequestValidator();

        [Test]
        public void AllFieldsEmpty()
        {
            var request = new PasswordChangeRequest();

            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
        }

        [Test]
        public void EmptyOldPassword()
        {
            var request = new PasswordChangeRequest()
            {
                OldPassword = "",
                NewPassword = "password",
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyNewPassword()
        {
            var request = new PasswordChangeRequest()
            {
                OldPassword = "password",
                NewPassword = "",
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullOldPassword()
        {
            var request = new PasswordChangeRequest()
            {
                OldPassword = null,
                NewPassword = "newPassword",
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullNewPassword()
        {
            var request = new PasswordChangeRequest()
            {
                OldPassword = "oldPassword",
                NewPassword = null,
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmptyPassword);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void OldAndNewPasswordsEqual()
        {
            var request = new PasswordChangeRequest()
            {
                OldPassword = "password",
                NewPassword = "password",
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.OldAndNewPasswordsEqual);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void ValidPasswordData()
        {
            var request = new PasswordChangeRequest()
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword",
            };

            Assert.DoesNotThrow(() => validator.ValidateAndThrow(request));
        }
    }
}
