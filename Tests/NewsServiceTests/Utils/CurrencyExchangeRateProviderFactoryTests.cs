using System;

using NUnit.Framework;

using Interfaces;
using CBIntegration;
using NewsService.Utils;
using DTO.NewsRequests;

namespace NewsServiceTests.Utils
{
    public class CurrencyExchangeRateProviderFactoryTests
    {
        private CurrencyExchangeRateProviderTypes centralBank = CurrencyExchangeRateProviderTypes.CentralBank;
        private ICurrencyExchangeRateProvider centralBankCurrencyrateProvider;

        [SetUp]
        public void Initialize()
        {
            centralBankCurrencyrateProvider = new RussianCBInfo();
        }

        [Test]
        public void CreateReturnsNotNullCurrencyRateProvider()
        {
            foreach (CurrencyExchangeRateProviderTypes type in Enum.GetValues(typeof(CurrencyExchangeRateProviderTypes)))
            {
                Assert.NotNull(CurrencyExchangeRateProviderFactory.Create(type));
            }
        }

        [Test]
        public void CreateCentralBankCurrencyRateProviderResultHasCorrectType()
        {
            var typeOfCreatedObject = CurrencyExchangeRateProviderFactory.Create(centralBank).GetType();

            Assert.AreEqual(typeOfCreatedObject, centralBankCurrencyrateProvider.GetType());
        }
    }
}
