using CBIntegration;
using DTO.NewsRequests;
using Interfaces;
using Kernel.CustomExceptions;

namespace NewsService.Utils
{
    public class CurrencyExchangeRateProviderFactory
    {
        public static ICurrencyExchangeRateProvider Create(CurrencyExchangeRateProviderTypes currencyRateProviderType)
        {
            return currencyRateProviderType switch
            {
                CurrencyExchangeRateProviderTypes.CentralBank =>
                    new RussianCBInfo(),
                _ =>
                    throw new BadRequestException("Invalid currency exchange rate provider type.")
            };
        }
    }
}
