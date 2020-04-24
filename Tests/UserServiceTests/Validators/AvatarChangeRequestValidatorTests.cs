using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class AvatarChangeRequestValidatorTests
    {
        private IValidator<AvatarChangeRequest> validator;
        private const int CMaxSizeOfImageInBytes = 2097152;

        [SetUp]
        public void Initialize()
        {
            validator = new AvatarChangeRequestValidator();
        }

        [Test]
        public void AvatarNullExtensionNotNull()
        {
            var request = new AvatarChangeRequest()
            {
                Avatar = null,
                AvatarExtension = "png"
            };
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.AvatarNullAvatarExtensionNotNull);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void ExtensionNullAvatarNotNull()
        {
            var request = new AvatarChangeRequest()
            {
                Avatar = new byte[5],
                AvatarExtension = null
            };
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.AvatarExtensionNullAvatarNotNull);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void AvatarExtensionNotCorrect()
        {
            var request = new AvatarChangeRequest()
            {
                Avatar = new byte[5],
                AvatarExtension = "abracadabra"
            };
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.AvatarExtensionNotCorrect);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void AvatarIsTooBig()
        {
            var request = new AvatarChangeRequest()
            {
                Avatar = new byte[CMaxSizeOfImageInBytes + 100],
                AvatarExtension = "png"
            };
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.AvatarTooBig);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void BothFieldsAreNullNotThrowAnything()
        {
            var request = new AvatarChangeRequest()
            {
                Avatar = null,
                AvatarExtension = null
            };
            Assert.DoesNotThrow(() => validator.ValidateAndThrow(request));
        }

        [Test]
        public void BothFieldsAreNotNullNotThrowAnything()
        {
            var request = new AvatarChangeRequest()
            {
                Avatar = new byte[5],
                AvatarExtension = "png"
            };
            Assert.DoesNotThrow(() => validator.ValidateAndThrow(request));
        }
    }
}
