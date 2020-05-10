using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using FluentValidation.Results;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UserService.Commands;
using UserService.Interfaces;

namespace UserServiceTests
{
    public class EditUserTests
    {
        private Mock<ILogger<EditUserCommand>> loggerMock;
        private Mock<IValidator<UserInfoRequest>> userValidatorMock;
        private Mock<IValidator<PasswordChangeRequest>> passwordValidatorMock;
        private Mock<IValidator<AvatarChangeRequest>> avatarValidatorMock;
        private Mock<IRequestClient<InternalEditUserInfoRequest>> requestClientMock;
        private Mock<Response<OperationResult<bool>>> resultMock;
        private IEditUserCommand command;
        private UserInfoRequest infoRequest;

        [SetUp]
        public void Init()
        {
            resultMock = new Mock<Response<OperationResult<bool>>>();
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool>() {Data = true});

            requestClientMock = new Mock<IRequestClient<InternalEditUserInfoRequest>>();
            requestClientMock
                .Setup(x => x.GetResponse<OperationResult<bool>>(It.IsAny<InternalEditUserInfoRequest>(), default, default))
                .Returns(Task.FromResult(resultMock.Object));

            userValidatorMock = new Mock<IValidator<UserInfoRequest>>();
            userValidatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());

            passwordValidatorMock = new Mock<IValidator<PasswordChangeRequest>>();
            passwordValidatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());

            avatarValidatorMock = new Mock<IValidator<AvatarChangeRequest>>();
            avatarValidatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());

            loggerMock = new Mock<ILogger<EditUserCommand>>();
            command = new EditUserCommand
                (userValidatorMock.Object, passwordValidatorMock.Object, 
                avatarValidatorMock.Object, requestClientMock.Object,
                loggerMock.Object);

            infoRequest = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla.bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-20)
            };
        }

        [Test]
        public void TestWithAllOptions()
        {
            var passwordRequest = new PasswordChangeRequest
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword",
            };
            var request = new EditUserRequest
            {
                PasswordRequest = passwordRequest,
                UserInfo = infoRequest
            };

            Assert.IsTrue(command.Execute(request).Result);
        }
        [Test]
        public void TestWithNullPassword()
        {
            var request = new EditUserRequest
            {
                PasswordRequest = null,
                UserInfo = infoRequest
            };

            Assert.IsTrue(command.Execute(request).Result);
        }

        [Test]
        public void TestWithErrorPasswordOptions()
        {
            var passwordRequest = new PasswordChangeRequest
            {
                OldPassword = null,
                NewPassword = "newPassword",
            };

            var request = new EditUserRequest
            {
                PasswordRequest = passwordRequest,
                UserInfo = infoRequest
            };

            Assert.ThrowsAsync<BadRequestException>(() => command.Execute(request));
        }
    }
}
