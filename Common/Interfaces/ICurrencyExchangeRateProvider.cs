using DTO.NewsRequests.Currency;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICurrencyExchangeRateProvider
    {
        List<ExchangeRate> GetCurrencies();
    }
}
