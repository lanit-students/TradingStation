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
        private CurrencyRateProviderTypes centralBank = CurrencyRateProviderTypes.CentralBank;
        private ICurrencyRateProvider centralBankCurrencyrateProvider;

        [SetUp]
        public void Initialize()
        {
            centralBankCurrencyrateProvider = new RussianCBInfo();
        }

        [Test]
        public void CreateReturnsNotNullCurrencyRateProvider()
        {
            foreach (CurrencyRateProviderTypes type in Enum.GetValues(typeof(CurrencyRateProviderTypes)))
            {
                Assert.NotNull(CurrencyRateProviderFactory.Create(type));
            }
        }

        [Test]
        public void CreateCentralBankCurrencyRateProviderResultHasCorrectType()
        {
            var typeOfCreatedObject = CurrencyRateProviderFactory.Create(centralBank).GetType();

            Assert.AreEqual(typeOfCreatedObject, centralBankCurrencyrateProvider.GetType());
        }
    }
}
