using Interfaces;
using NewsService;
using NewsService.Utils;
using NUnit.Framework;
using CentralBankIntegration;
using System;

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
        public void CreateNotNullNewsPublisher()
        {
            foreach (NewsPublisherTypes type in Enum.GetValues(typeof(NewsPublisherTypes)))
            {
                Assert.NotNull(NewsPublisherFactory.Create(type));
            }
        }

        [Test]
        public void CreateCentralBankNewsPublisherResultHasCorrectedType()
        {
            Assert.AreEqual((NewsPublisherFactory.Create(centralBankNewsPublisherType)).GetType(), centralBankNewsPublisher.GetType());
        }
    }
}
