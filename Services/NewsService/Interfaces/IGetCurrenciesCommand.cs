using DTO.NewsRequests.Currency;
using System.Collections.Generic;

namespace NewsService.Interfaces
{
    public interface IGetCurrenciesCommand
    {
        List<ExchangeRate> Execute(CurrencyRequest requestParams, IEqualityComparer<string> comparer);
    }
}
