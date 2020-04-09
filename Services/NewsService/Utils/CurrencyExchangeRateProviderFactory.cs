using CBIntegration;
using DTO.NewsRequests;
using Interfaces;
using Kernel.CustomExceptions;
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
                    throw new BadRequestException("Invalid currency exchange rate provider type.");
            }
        }
    }
}
