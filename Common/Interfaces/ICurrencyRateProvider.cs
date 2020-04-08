using DTO.NewsRequests.Currency;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICurrencyRateProvider
    {
        List<ExchangeRate> GetCurrencies();
    }
}
