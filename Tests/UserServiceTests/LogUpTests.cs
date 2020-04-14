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
using Kernel;
using System;

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
            request.Password = "123";
            request.LastName = "Mjad";
            request.FirstName = "Drad";
            request.Email = "edfs@mail.ru";
            request.Birthday = DateTime.Today.AddYears(-20);

            validatorMock = new Mock<IValidator<CreateUserRequest>>();
            validatorMock
                .Setup(x => x.Validate(request))
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
