using Interfaces;
using NewsService;
using NewsService.Utils;
using NUnit.Framework;
using CentralBankIntegration;
namespace NewsServiceTests.Utils
{
    class NewsPublisherFactoryTests
    {
        private NewsPublisherTypes centralBankNewsPublisherType = NewsPublisherTypes.CentralBank;
        private INewsPublisher centralBankNewsPublisher;

        [SetUp]
        public void Initialize()
        {
            centralBankNewsPublisher = new CentralBankNewsPublisher();
        }

        [Test]
        public void CreateCBNewsPublisherTest()
        {
            Assert.AreEqual((NewsPublisherFactory.Create(centralBankNewsPublisherType)).GetType(), centralBankNewsPublisher.GetType());
        }
    }
}
