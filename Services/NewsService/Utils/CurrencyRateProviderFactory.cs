using CBIntegration;
using DTO.NewsRequests;
using Interfaces;
using System;

namespace NewsService.Utils
{
    public class CurrencyRateProviderFactory
    {
        public static ICurrencyRateProvider Create(CurrencyRateProviderTypes currencyRateProviderType)
        {
            switch (currencyRateProviderType)
            {
                case CurrencyRateProviderTypes.CentralBank:
                    return new RussianCBInfo();
                default:
                    //TODO change on custom exception
                    throw new NotImplementedException();
            }
        }
    }
}
