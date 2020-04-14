using NUnit.Framework;
using UserService.Interfaces;
using UserService.Commands;
using UserService.Validators;
using FluentValidation;
using DTO.RestRequests;
using Moq;
using MassTransit;
using DTO.BrokerRequests;
using DTO;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace UserServiceTests
{
   public class LogUpTests
    {
        private ICreateUserCommand command;
        private Mock<IValidator<CreateUserRequest>> validatorMock;
        private Mock<IRequestClient<InternalCreateUserRequest>> clientMock;
        private CreateUserRequest request;
        private Mock<Response<OperationResult>> responceMock;
        
        [SetUp]
        public void Initialization()
        {
            request = new CreateUserRequest();

            validatorMock = new Mock<IValidator<CreateUserRequest>>();
            validatorMock
                .Setup(x => x.Validate(new ValidationContext(request)))
                .Returns(new ValidationResult());
                

            responceMock = new Mock<Response<OperationResult>>();
            responceMock
                .Setup(x => x.Message)
                .Returns(new OperationResult() { IsSuccess = true});

            clientMock = new Mock<IRequestClient<InternalCreateUserRequest>>();
            clientMock
                .Setup(x => x.GetResponse<OperationResult>(new InternalCreateUserRequest(), default, default))
                .Returns(Task.FromResult(responceMock.Object));

            command = new CreateUserCommand(clientMock.Object, validatorMock.Object);
        }

        [Test]
        public void Correct()
        {
            var isUser = command.Execute(request).Result;

            Assert.AreEqual(true, isUser);
        }
    }
}
