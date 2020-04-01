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
        public void CreateReturnsNotNullNewsPublisher()
        {
            foreach (NewsPublisherTypes type in Enum.GetValues(typeof(NewsPublisherTypes)))
            {
                Assert.NotNull(NewsPublisherFactory.Create(type));
            }
        }

        [Test]
        public void CreateCentralBankNewsPublisherResultHasCorrectType()
        {
            var typeOfCreatedObject = NewsPublisherFactory.Create(centralBankNewsPublisherType).GetType();
            Assert.AreEqual(typeOfCreatedObject, centralBankNewsPublisher.GetType());
        }
    }
}
