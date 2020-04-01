
using NewsService;
using NewsService.Utils;
using NUnit.Framework;

namespace NewsServiceTests.Utils
{
    class NewsPublisherFactoryTests
    {
        private NewsPublisherTypes centralBankNewsPublisher = NewsPublisherTypes.CentralBank;

        [SetUp]
        public void Initialize()
        {
        }

        [Test]
        public void CreateCBNewsPublisherTest()
        {
            Assert.NotNull(NewsPublisherFactory.Create(centralBankNewsPublisher));
        }
    }
}
