using CBIntegration;
using DTO.NewsRequests;
using Interfaces;
using System;

namespace NewsService.Utils
{
    public class CurrencyExchangeRateProviderFactory
    {
        public static ICurrencyExchangeRateProvider Create(CurrencyExchangeRateProviderTypes currencyRateProviderType)
        {
            switch (currencyRateProviderType)
            {
                case CurrencyExchangeRateProviderTypes.CentralBank:
                    return new RussianCBInfo();
                default:
                    //TODO change on custom exception
                    throw new NotImplementedException();
            }
        }
    }
}
