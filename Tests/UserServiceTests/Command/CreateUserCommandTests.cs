using NUnit.Framework;
using UserService.Interfaces;
using UserService.Commands;
using FluentValidation;
using DTO.RestRequests;
using Moq;
using MassTransit;
using DTO.BrokerRequests;
using DTO;
using System.Threading.Tasks;
using FluentValidation.Results;
using Kernel.CustomExceptions;
using Microsoft.Extensions.Logging;
using System;

namespace UserServiceTests
{
    public class CreateUserCommandTests
     {
        private ICreateUserCommand command;
        private Mock<IValidator<CreateUserRequest>> validatorMock;
        private Mock<IRequestClient<InternalCreateUserRequest>> clientMock;
        private Mock<ISecretTokenEngine> secretTokenMock;
        private Mock<ILogger<CreateUserCommand>> loggerMock;
        private Mock<Response<OperationResult<bool>>> responseMock;
        private Mock<ValidationResult> validatorResultMock;
        private CreateUserRequest request;
        private Mock<IEmailSender> emailSenderMock;
        Guid token;

        [SetUp]
        public void Initialization()
        {
            request = new CreateUserRequest();
            request.Password = "Not empty string";
            request.Email = "TraidPlatform@mail.ru";
            token = Guid.NewGuid();
            validatorResultMock = new Mock<ValidationResult>();
            validatorResultMock
                .Setup(x => x.IsValid)
                .Returns(true);

            validatorMock = new Mock<IValidator<CreateUserRequest>>();
            validatorMock
                .Setup(x => x.Validate(It.IsAny<ValidationContext>()))
                .Returns(validatorResultMock.Object);

            responseMock = new Mock<Response<OperationResult<bool>>>();
            responseMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool>() { Data = true});

            clientMock = new Mock<IRequestClient<InternalCreateUserRequest>>();
            clientMock
                .Setup(x => x.GetResponse<OperationResult<bool>>(It.IsAny<InternalCreateUserRequest>(), default, default))
                .Returns(Task.FromResult(responseMock.Object));

            secretTokenMock = new Mock<ISecretTokenEngine>();
            secretTokenMock
             .Setup(x => x.GetToken(It.IsAny<string>()))
             .Returns(token);
            loggerMock = new Mock<ILogger<CreateUserCommand>>();

            emailSenderMock = new Mock<IEmailSender>();
            emailSenderMock
                .Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<ISecretTokenEngine>()));
            command = new CreateUserCommand(clientMock.Object, validatorMock.Object,secretTokenMock.Object,loggerMock.Object, emailSenderMock.Object);
        }

        [Test]
        public void CreateUserCommandValidationExceptionIncorrectUserData()
        {
            validatorResultMock
                .Setup(x => x.IsValid)
                .Returns(false);

            Assert.ThrowsAsync<ValidationException>(async () => await command.Execute(request));
        }

        [Test]
        public void CreateUserCommandBadRequestExceptionUnableToCreateUser()
        {
            responseMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool>() { StatusCode = 400 });

            Assert.ThrowsAsync<BadRequestException>(async () => await command.Execute(request));
        }

        [Test]
        public void CreateUserCommandTrueSuccessfulUserCreation()
        {

            Assert.IsTrue(command.Execute(request).Result);
        }
     }
}
