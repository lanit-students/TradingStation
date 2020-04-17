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

namespace UserServiceTests
{
     public class CreateUserCommandTests
     {
        private ICreateUserCommand command;
        private Mock<IValidator<CreateUserRequest>> validatorMock;
        private Mock<IRequestClient<InternalCreateUserRequest>> clientMock;
        private Mock<Response<OperationResult<bool>>> responseMock;
        private Mock<ValidationResult> validatorResultMock;
        private CreateUserRequest request;

        [SetUp]
        public void Initialization()
        {
            request = new CreateUserRequest();
            request.Password = "Not empty string";

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

            command = new CreateUserCommand(clientMock.Object, validatorMock.Object);
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
