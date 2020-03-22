using NUnit.Framework;

using AuthenticationService;
using AuthenticationService.Interfaces;
using System;

namespace AuthenticationServiceTests.Utils
{
    public class TokensEngineTests
    {
        private readonly Guid goodUserId = Guid.NewGuid();

        private ITokensEngine tokensEngine;

        [SetUp]
        public void Initialize()
        {
            tokensEngine = new TokensEngine();
        }

        [Test]
        public void TokensEngineGetTokenTest()
        {
            Assert.NotNull(tokensEngine.GetToken(goodUserId));
        }
    }
}
