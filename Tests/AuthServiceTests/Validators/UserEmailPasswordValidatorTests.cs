using AuthenticationService.Validators;
using DTO;
using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace AuthenticationServiceTests.Validators
{
    public class UserEmailPasswordValidatorTests
    {
        private UserEmailPassword emailPasswordEmpty = new UserEmailPassword(string.Empty, string.Empty);

        private UserEmailPassword emailEmptyPasswordOk = new UserEmailPassword(string.Empty, "xxxxx");

        private UserEmailPassword passwordEmptyEmailOk = new UserEmailPassword("example@gmail.com", string.Empty);

        private UserEmailPassword emailInvalidFormat = new UserEmailPassword("ololo", "xxxxx");

        private UserEmailPassword passwordInvalidFormat = new UserEmailPassword("example@gmail.com", "/-=%");

        private UserEmailPassword passwordTooShort = new UserEmailPassword("example@gmail.com", "a");

        private UserEmailPassword passwordTooLong = new UserEmailPassword("example@gmail.com", new string('c', 501));

        private IValidator<UserEmailPassword> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new UserEmailPasswordValidator();
        }

        [Test]
        public void UserEmailPasswordAllEmpty()
        {
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(emailPasswordEmpty));
        }

        [Test]
        public void UserEmailPasswordEmptyEmail()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(emailEmptyPasswordOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Email address must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordEmptyPassword()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordEmptyEmailOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordInvalidEmailFormat()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(emailInvalidFormat));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Email address must be in valid format.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordInvalidPasswordFormat()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordInvalidFormat));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password must be alphanumeric.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordTooShortPassword()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordTooShort));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password's length must be from 5 to 500 symbols.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordTooLongPassword()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordTooLong));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password's length must be from 5 to 500 symbols.");

            Assert.IsNotNull(expectedError);
        }
    }
}
