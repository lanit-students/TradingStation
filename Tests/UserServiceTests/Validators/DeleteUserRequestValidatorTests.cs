using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class DeleteUserRequestValidatorTests
    {
        private IValidator<UserIdRequest> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new UserIdRequestValidator();
        }
        
        [Test]
        public void IdIsNull()
        {
            var request = new UserIdRequest();
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
        }

        [Test]
        public void IdIsEmpty()
        {
            var request = new UserIdRequest { UserId = Guid.Empty };
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
        }

        [Test]
        public void IdIsOk()
        {
            var request = new UserIdRequest { UserId = Guid.NewGuid() };
            Assert.DoesNotThrow(() => validator.ValidateAndThrow(request));
        }
    }
}
