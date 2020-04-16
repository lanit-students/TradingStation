using System;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using FluentValidation.Results;
using IDeleteUserUserService.Interfaces;
using Kernel.CustomExceptions;
using MassTransit;
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

        private IDeleteUserCommand command;

        private Mock<IRequestClient<InternalDeleteUserRequest>> requestClientMock;
        private Mock<Response<OperationResult>> resultMock;
        private Mock<IValidator<DeleteUserRequest>> validatorMock;

        [SetUp]
        public void Init()
        {
            validatorMock = new Mock<IValidator<DeleteUserRequest>>();
            validatorMock
                .Setup(x =>
                    x.Validate(It.IsAny<ValidationContext>()))
                .Returns(new ValidationResult());

            resultMock = new Mock<Response<OperationResult>>();

            requestClientMock = new Mock<IRequestClient<InternalDeleteUserRequest>>();
            requestClientMock
                .Setup(x =>
                    x.GetResponse<OperationResult>(
                        It.IsAny<InternalDeleteUserRequest>(), default, default))
                .ReturnsAsync(resultMock.Object);
        }

        [Test]
        public void ValidateThrowException()
        {
            var request = new DeleteUserRequest();
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult {IsSuccess = true});

            validatorMock
                .Setup(x =>
                    x.Validate(It.IsAny<ValidationContext>()))
                .Throws(new ValidationException("error"));

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object);

            Assert.ThrowsAsync<ValidationException>(() => command.Execute(request));
        }

        [Test]
        public void DataBaseThrowNotFoundException()
        {
            resultMock
                .Setup(x => x.Message)
                .Throws(new NotFoundException());

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object);

            Assert.ThrowsAsync<NotFoundException>(() => command.Execute(request));
        }

        [Test]
        public void DataBaseThrowBadRequestException()
        {
            resultMock
                .Setup(x => x.Message)
                .Throws(new BadRequestException());

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object);

            Assert.ThrowsAsync<BadRequestException>(() => command.Execute(request));
        }

        [Test]
        public void ResponseFromDataBaseIsFalse()
        {
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult {IsSuccess = false});

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object);

            Assert.ThrowsAsync<BadRequestException>(() => command.Execute(request));
        }

        [Test]
        public void ValidTest()
        {
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult {IsSuccess = true});

            var command = new DeleteUserCommand(requestClientMock.Object, validatorMock.Object);

            Assert.True(command.Execute(request).Result);
        }
    }
}