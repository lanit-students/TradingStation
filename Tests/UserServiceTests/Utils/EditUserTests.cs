using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using FluentValidation.Results;
using MassTransit;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Commands;
using UserService.Interfaces;

namespace UserServiceTests
{
    class EditUserTests
    {
        private Mock<IValidator<UserInfoRequest>> userValidatorMock;
        private Mock<IValidator<PasswordChangeRequest>> passwordValidatorMock;
        private Mock<IRequestClient<InternalEditUserInfoRequest>> requestClientMock;
        private Mock<Response<OperationResult>> resultMock;
        private IEditUserCommand command;

        [SetUp]
        public void Init()
        {
            OperationResult operation = new OperationResult();
            operation.IsSuccess = true;
            resultMock = new Mock<Response<OperationResult>>();
            resultMock
                .Setup(x => x.Message)
                .Returns(operation);
            requestClientMock = new Mock<IRequestClient<InternalEditUserInfoRequest>>();
            requestClientMock
                .Setup(x => x.GetResponse<OperationResult>(It.IsAny<InternalEditUserInfoRequest>(), default, default))
                .Returns(Task.FromResult(resultMock.Object));
            userValidatorMock = new Mock<IValidator<UserInfoRequest>>();
            userValidatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());
            passwordValidatorMock = new Mock<IValidator<PasswordChangeRequest>>();
            passwordValidatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());
            command = new EditUserCommand(userValidatorMock.Object, passwordValidatorMock.Object, requestClientMock.Object);
        }

        [Test]
        public void TestWithAllOptions()
        {
            var infoRequest = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla.bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-20)
            };

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
            var infoRequest = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla.bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-20)
            };

            var request = new EditUserRequest
            {
                PasswordRequest = null,
                UserInfo = infoRequest
            };

            Assert.IsTrue(command.Execute(request).Result);
        }
    }
}
