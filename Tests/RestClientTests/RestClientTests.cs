using NUnit.Framework;
using RestClient;

namespace RestClientTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateNewRestClientTest()
        {
            Assert.NotNull(new RestClient());
        }
    }
}