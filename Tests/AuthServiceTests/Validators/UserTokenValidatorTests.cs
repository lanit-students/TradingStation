using AuthenticationService.Validators;
using DTO;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Linq;

namespace AuthenticationServiceTests.Validators
{
    public class UserTokenValidatorTests
    {
        private UserToken tokenBadEverything = new UserToken
        {
            Body = string.Empty,
            UserId = Guid.Empty,
        };

        private UserToken tokenBadBody = new UserToken
        {
            UserId = Guid.NewGuid()
        };

        private IValidator<UserToken> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new UserTokenValidator();
        }

        [Test]
        public void UserTokenValidateAllBad()
        {
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(tokenBadEverything));
        }

        [Test]
        public void UserTokenValidateBadBody()
        {
            var exception = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(tokenBadBody));

            Assert.AreEqual("Token body must not be empty.", exception.Errors.FirstOrDefault().ErrorMessage);
        }
    }
}
