using AuthenticationService.Utils;
using DTO;
using Kernel.CustomExceptions;
using MassTransit;
using Moq;
using NUnit.Framework;
using System;
using UserService.Commands;

namespace UserServiceTests.Commands
{
    public class CreateUserCommandTests
    {
        private UserEmailPassword goodUserEmailPassword = new UserEmailPassword 
        { 
            Email = "bla@bla.com", 
            PasswordHash = ShaHash.GetPasswordHash("hash") 
        };
        private UserEmailPassword badUserEmailPassword = new UserEmailPassword 
        { 
            Email = null, 
            PasswordHash = null 
        };
        private User goodUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "bla@bla.com",
            PasswordHash = ShaHash.GetPasswordHash("hash") 
        };
        private Mock<IBus> busControl;
        // Not sure should we use command or mock command
        private CreateUserCommand command;
        private Mock<CreateUserCommand> mockCommand;

        [SetUp]
        public void Initialize()
        {
            busControl = new Mock<IBus>();
            command = new CreateUserCommand(busControl.Object);

            mockCommand = new Mock<CreateUserCommand>();
            mockCommand.Setup(command => command.Execute(badUserEmailPassword)).Throws(new BadRequestException());
            mockCommand.Setup(command => command.Execute(goodUserEmailPassword)).ReturnsAsync(Guid.NewGuid());

            var uri = new Uri("rabbitmq://localhost/DatabaseServiceCreate");
            // Commented because of error
            //busControl.Setup(bus => bus.CreateRequestClient<UserEmailPassword>(uri).Create(badUserEmailPassword)
            //                .GetResponse<User>(true).Result.Message).Throws(new BadRequestException());
            //busControl.Setup(bus => bus.CreateRequestClient<UserEmailPassword>(uri).Create(goodUserEmailPassword)
            //                .GetResponse<User>(true).Result.Message).Returns(goodUser);
        }

        [Test]
        public void ExecuteBadUserReturnBadRequestException()
        {
            Assert.Throws<Exception>(() => command.Execute(badUserEmailPassword));
            // Assert.Throws<BadRequestException>(() => mockCommand.Object.Execute(badUserEmailPassword));
        }


        [Test]
        public void ExecuteGoodUserReturnNewGuid()
        {
            var id = command.Execute(goodUserEmailPassword).Result;
            // id = mockCommand.Object.Execute(goodUserEmailPassword).Result;
            Assert.NotNull(id);
            Assert.AreEqual(id.GetType(), typeof(Guid));
        }

        [Test]
        public void CreateBadUserInDBSSendUserReturnException()
        {
            // We don't test private methods?
            Assert.Throws<BadRequestException>(() => new BadRequestException());
        }

        [Test]
        public void CreateUserInDBSSendGoodUserEmailPasswordReturnUser()
        {
            // We don't test private methods?
        }

    }
}
