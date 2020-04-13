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

namespace UserServiceTests
{
   public class LogUpTests
    {
        private ICreateUserCommand command;
        private IValidator<CreateUserRequest> validator;
        private Mock<IRequestClient<InternalCreateUserRequest>> clientMock;
        private InternalCreateUserRequest request;
        private bool response;
        private Mock<Response<OperationResult>> responceMock;
        
        [SetUp]
        public void Initialization()
        {
            request = new InternalCreateUserRequest();
            response = new bool();
            validator = new CreateUserRequestValidator();

            responceMock = new Mock<Response<OperationResult>>();
            responceMock
                .Setup(x => x.Message.IsSuccess)
                .Returns(response);

            clientMock = new Mock<IRequestClient<InternalCreateUserRequest>>();
            clientMock
                .Setup(x => x.GetResponse<OperationResult>(request, default, default))
                .Returns(Task.FromResult(responceMock.Object));

            command = new CreateUserCommand(clientMock.Object, validator);
        }
    }
}
