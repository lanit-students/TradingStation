using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class DeleteUserRequestValidatorTests
    {
        private IValidator<DeleteUserRequest> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new DeleteUserRequestValidator();
        }
        
        [Test]
        public void IdIsNull()
        {
            var request = new DeleteUserRequest();
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
        }

        [Test]
        public void IdIsEmpty()
        {
            var request = new DeleteUserRequest { UserId = Guid.Empty };
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
        }

        [Test]
        public void IdIsOk()
        {
            var request = new DeleteUserRequest { UserId = Guid.NewGuid() };
            Assert.DoesNotThrow(() => validator.ValidateAndThrow(request));
        }
    }
}
