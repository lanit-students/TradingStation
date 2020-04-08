using System;

using NUnit.Framework;

using Interfaces;
using CBIntegration;
using NewsService.Utils;
using DTO.NewsRequests;

namespace NewsServiceTests.Utils
{
    public class CurrencyRateProviderFactoryTests
    {
        private CurrencyRateProviderTypes centralBankType = CurrencyRateProviderTypes.CentralBank;
        private ICurrencyRateProvider centralBankNewsPublisher;

        [SetUp]
        public void Initialize()
        {
            centralBankNewsPublisher = new RussianCBInfo();
        }

        [Test]
        public void CreateReturnsNotNullNewsPublisher()
        {
            foreach (CurrencyRateProviderTypes type in Enum.GetValues(typeof(CurrencyRateProviderTypes)))
            {
                Assert.NotNull(CurrencyRateProviderFactory.Create(type));
            }
        }

        [Test]
        public void CreateCentralBankNewsPublisherResultHasCorrectType()
        {
            var typeOfCreatedObject = CurrencyRateProviderFactory.Create(centralBankType).GetType();

            Assert.AreEqual(typeOfCreatedObject, centralBankNewsPublisher.GetType());
        }
    }
}
