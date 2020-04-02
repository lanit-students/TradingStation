using AuthenticationService.Validators;
using DTO;
using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace AuthenticationServiceTests.Validators
{
    public class UserEmailPasswordValidatorTests
    {
        private UserEmailPassword emailPasswordHashEmpty = new UserEmailPassword(string.Empty, string.Empty);

        private UserEmailPassword emailEmptyPasswordHashOk = new UserEmailPassword(string.Empty, new string('c', 40));

        private UserEmailPassword passwordHashEmptyEmailOk = new UserEmailPassword("example@gmail.com", string.Empty);

        private UserEmailPassword emailInvalidFormatPasswordHashOk = new UserEmailPassword("ololo", new string('c', 40));

        private UserEmailPassword passwordHashTooShortEmailOk = new UserEmailPassword("example@gmail.com", "a");

        private UserEmailPassword passwordHashTooLongEmailOk = new UserEmailPassword("example@gmail.com", new string('c', 41));

        private IValidator<UserEmailPassword> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new UserEmailPasswordValidator();
        }

        [Test]
        public void UserEmailPasswordAllEmpty()
        {
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(emailPasswordHashEmpty));
        }

        [Test]
        public void UserEmailPasswordEmptyEmail()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(emailEmptyPasswordHashOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Email address must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordEmptyPassword()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordHashEmptyEmailOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password hash must not be empty.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordInvalidEmailFormat()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(emailInvalidFormatPasswordHashOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Email address must be in valid format.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordTooShortPassword()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordHashTooShortEmailOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password's hash length must be 40 symbols.");

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void UserEmailPasswordTooLongPassword()
        {
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(passwordHashTooLongEmailOk));

            var expectedError = validationResult.Errors.FirstOrDefault(error => error.ErrorMessage == "Password's hash length must be 40 symbols.");

            Assert.IsNotNull(expectedError);
        }
    }
}