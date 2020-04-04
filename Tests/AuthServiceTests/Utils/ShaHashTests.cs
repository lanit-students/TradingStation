using Kernel;
using Kernel.CustomExceptions;
using NUnit.Framework;

namespace AuthenticationServiceTests.Utils
{
    public class ShaHashTests
    {
        [Test]
        public void GetPasswordHashNormalPasswordTest()
        {
            Assert.IsNotEmpty(ShaHash.GetPasswordHash("password"));
        }

        [Test]
        public void GetPasswordHashEmptyPasswordTest()
        {
            Assert.Throws<BadRequestException>(() => ShaHash.GetPasswordHash(null));
        }
    }
}
