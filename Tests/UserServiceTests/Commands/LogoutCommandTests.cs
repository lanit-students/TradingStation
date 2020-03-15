using System;

using Moq;
using NUnit.Framework;

using AuthenticationService.Commands;
using AuthenticationService.Interfaces;

namespace UserServiceTests.Commands
{
    public class LogoutCommandTests
    {
        private const int BadId = -1;

        private Mock<ITokensEngine> tokensEngine;
        private ICommand<int> command;

        [SetUp]
        public void Initialize()
        {
            tokensEngine = new Mock<ITokensEngine>();

            command = new LogoutCommand(tokensEngine.Object);
        }

        [Test]
        public void LoginCommandBadIdTest()
        {
            Assert.Throws<Exception>(() => command.Execute(BadId));
        }
    }
}
