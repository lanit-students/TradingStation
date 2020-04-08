using System;

using NUnit.Framework;

using Interfaces;
using CBIntegration;
using NewsService.Utils;

namespace NewsServiceTests.Utils
{
    public class NewsPublisherFactoryTests
    {
        private NewsPublisherTypes centralBankNewsPublisherType = NewsPublisherTypes.CentralBank;
        private INewsPublisher centralBankNewsPublisher;

        [SetUp]
        public void Initialize()
        {
            centralBankNewsPublisher = new RussianCBInfo();
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
