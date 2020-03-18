using NUnit.Framework;

using AuthenticationService;
using AuthenticationService.Interfaces;

namespace UserServiceTests.Utils
{
    public class TokensEngineTests
    {
        private const int GoodId = 1;

        private ITokensEngine tokensEngine;

        [SetUp]
        public void Initialize()
        {
            tokensEngine = new TokensEngine();
        }

        [Test]
        public void TokensEngineGetTokenTest()
        {
            Assert.NotNull(tokensEngine.GetToken(GoodId));
        }
    }
}
