using DTO.NewsRequests.Currency;
using NewsService.Interfaces;
using NewsService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsService.Commands
{
    public class GetCurrenciesCommand : IGetCurrenciesCommand
    {
        public List<ExchangeRate> Execute(CurrencyRequest requestParams, IEqualityComparer<string> comparer)
        {
            List<ExchangeRate> rates = CurrencyRateProviderFactory
                .Create(requestParams.CurrencyPublisher)
                .GetCurrencies();

            return rates
                .Where(r => requestParams.CurrencyCodes.Contains(r.Code, comparer))
                .ToList();
        }
    }
}
