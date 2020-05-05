using AuthenticationService;
using AuthenticationService.Commands;
using AuthenticationService.Interfaces;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AuthenticationServiceTests
{
    public class LoginTests
    {
        private Mock<IRequestClient<InternalLoginRequest>> clientMock;
        private Mock<Response<OperationResult<UserCredential>>> responseMock;
        private ITokensEngine tokensEngine;
        private ILoginCommand command;
        private LoginRequest request;
        private UserCredential response;

        [SetUp]
        public void Initialize()
        {
            tokensEngine = new TokensEngine();
            request = new LoginRequest();
            response = new UserCredential();

            responseMock = new Mock<Response<OperationResult<UserCredential>>>();
            responseMock
                .Setup(x => x.Message)
                .Returns(new OperationResult<UserCredential>() { Data = response });

            clientMock = new Mock<IRequestClient<InternalLoginRequest>>();
            clientMock
                .Setup(x => x.GetResponse<OperationResult<UserCredential>>(It.IsAny<InternalLoginRequest>(), default, default))
                .Returns(Task.FromResult(responseMock.Object));

            command = new LoginCommand(tokensEngine, clientMock.Object, new Mock<ILogger<LoginCommand>>().Object);
        }

        [Test]
        public void InvalidPasswordLoginThrowsForbiddenException()
        {
            request.Email = "a@a.a";
            request.Password = "aaa";
            response.Email = "a@a.a";
            response.PasswordHash = ShaHash.GetPasswordHash("bbb");

            Assert.ThrowsAsync<ForbiddenException>(async () => await command.Execute(request));
        }

        [Test]
        public void EmailCheckFailLoginThrowsForbiddenException()
        {
            request.Email = "a@a.a";
            request.Password = "aaa";
            response.Email = "b@b.b";
            response.PasswordHash = ShaHash.GetPasswordHash("aaa");

            Assert.ThrowsAsync<ForbiddenException>(async () => await command.Execute(request));
        }

        [Test]
        public void CorrectLoginDataReturnsValidToken()
        {
            request.Email = "a@a.a";
            request.Password = "aaa";
            response.Email = "a@a.a";
            response.PasswordHash = ShaHash.GetPasswordHash("aaa");
            response.UserId = Guid.NewGuid();

            var userToken = command.Execute(request).Result;

            Assert.IsTrue(tokensEngine.CheckToken(userToken).IsSuccess);
        }
    }
}
