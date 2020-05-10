using DTO;
using DTO.BrokerRequests;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UserService.Commands;
using UserService.Interfaces;

namespace UserServiceTests.Commands
{
    public class ConfirmUserComandTests
    {
        private IConfirmUserCommand command;
        private Mock<IRequestClient<InternalConfirmUserRequest>> clientMock;
        private Mock<ISecretTokenEngine> secretTokenEngineMock;
        private Mock<Response<OperationResult<bool>>> resultMock;
        private Mock<ILogger<ConfirmUserCommand>> loggerMock;
        string email;
        Guid token;

        [SetUp]
        public void Initialization()
        {
            email = "example@mail.ru";
            token = Guid.NewGuid();

            resultMock = new Mock<Response<OperationResult<bool>>>();

            clientMock = new Mock<IRequestClient<InternalConfirmUserRequest>>();
            clientMock
                .Setup(x => x.GetResponse<OperationResult<bool>>(It.IsAny<InternalConfirmUserRequest>(), default, default))
                .Returns(Task.FromResult(resultMock.Object));

            secretTokenEngineMock = new Mock<ISecretTokenEngine>();
            secretTokenEngineMock
                .Setup(x => x.GetEmail(It.IsAny<Guid>()))
                .Returns(email);

            loggerMock = new Mock<ILogger<ConfirmUserCommand>>();

            command = new ConfirmUserCommand(clientMock.Object, secretTokenEngineMock.Object, loggerMock.Object);
        }

        [Test]
        public void ConfirmUserCommandSuccessesfulUserConfirm()
        {
            resultMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<bool> { Data = true });

            Assert.AreEqual(command.Execute(token).Result, true);
        }

        [Test]
         public void ConfirmUserBadRequest()
        {
            resultMock
               .Setup(x => x.Message)
               .Throws(new BadRequestException());

            Assert.ThrowsAsync<BadRequestException>(() => command.Execute(token));
        }
    }
}




