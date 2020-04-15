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
        private Mock<Response<OperationResult>> responseMock;
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
                
            responseMock = new Mock<Response<OperationResult>>();
            responseMock
                .Setup(x => x.Message)
                .Returns(new OperationResult() { IsSuccess = true});

            clientMock = new Mock<IRequestClient<InternalCreateUserRequest>>();
            clientMock
                .Setup(x => x.GetResponse<OperationResult>(It.IsAny<InternalCreateUserRequest>(), default, default))
                .Returns(Task.FromResult(responseMock.Object));

            command = new CreateUserCommand(clientMock.Object, validatorMock.Object);
        }

        [Test]
        public void CreateUserCommand_ValidationException_IncorrectUserData()
        {
            validatorResultMock
                .Setup(x => x.IsValid)
                .Returns(false);

            Assert.ThrowsAsync<ValidationException>(async () => await command.Execute(request));
        }

        [Test]
        public void CreateUserCommand_BadRequestException_UnableToCreateUser()
        {
            responseMock
                .Setup(x => x.Message)
                .Returns(new OperationResult() { IsSuccess = false });

            Assert.ThrowsAsync<BadRequestException>(async () => await command.Execute(request));
        }

        [Test]
        public void CreateUserCommand_True_SuccessfulUserCreation()
        {
            Assert.IsTrue(command.Execute(request).Result);
        }
     }
}
