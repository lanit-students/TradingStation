using System;
using System.Threading.Tasks;
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
using UserService.Commands;

namespace UserServiceTests.Commands
{
    internal class DeleteUserCommandTest
    {
        private readonly DeleteUserRequest request = new DeleteUserRequest
        {
            UserId = Guid.NewGuid()
        };

        private Mock<IRequestClient<InternalDeleteUserRequest>> requestClientMock;
        private Mock<Response<OperationResult<bool>>> resultMock;
        private Mock<IValidator<DeleteUserRequest>> validatorMock;

        [SetUp]
        public void Init()
        {
            validatorMock = new Mock<IValidator<DeleteUserRequest>>();
            validatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());

            resultMock = new Mock<Response<OperationResult<bool>>>();

            requestClientMock = new Mock<IRequestClient<InternalDeleteUserRequest>>();
            requestClientMock
                .Setup(x => x.GetResponse<OperationResult<bool>>(It.IsAny<InternalDeleteUserRequest>(), default, default))
                .Returns(Task.FromResult(resultMock.Object));
        }

        [Test]
        public void ValidateThrowException()
        {
            var request = new DeleteUserRequest();
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool> { Data = true });

            validatorMock
                .Setup(x =>
                    x.Validate(It.IsAny<ValidationContext>()))
                .Throws(new ValidationException("error"));

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object, new Mock<ILogger<DeleteUserCommand>>().Object);

            Assert.ThrowsAsync<ValidationException>(() => command.Execute(request));
        }

        [Test]
        public void DataBaseThrowNotFoundException()
        {
            resultMock
                .Setup(x => x.Message)
                .Throws(new NotFoundException());

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object, new Mock<ILogger<DeleteUserCommand>>().Object);

            Assert.ThrowsAsync<NotFoundException>(() => command.Execute(request));
        }

        [Test]
        public void DataBaseThrowBadRequestException()
        {
            resultMock
                .Setup(x => x.Message)
                .Throws(new BadRequestException());

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object, new Mock<ILogger<DeleteUserCommand>>().Object);

            Assert.ThrowsAsync<BadRequestException>(() => command.Execute(request));
        }

        [Test]
        public void ResponseFromDataBaseIsFalse()
        {
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool> { StatusCode = 400 });

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object, new Mock<ILogger<DeleteUserCommand>>().Object);

            Assert.ThrowsAsync<BadRequestException>(() => command.Execute(request));
        }

        [Test]
        public void ValidTest()
        {
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool> { Data = true });

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object, new Mock<ILogger<DeleteUserCommand>>().Object);

            Assert.True(command.Execute(request).Result);
        }
    }
}