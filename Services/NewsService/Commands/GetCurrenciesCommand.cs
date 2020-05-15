using DTO.NewsRequests.Currency;
using NewsService.Interfaces;
using NewsService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NewsService.Commands
{
    public class GetCurrenciesCommand : IGetCurrenciesCommand
    {
        private readonly IValidator<CurrencyRequest> validator;

        public GetCurrenciesCommand([FromServices] IValidator<CurrencyRequest> validator)
        {
            this.validator = validator;
        }

        public List<ExchangeRate> Execute(CurrencyRequest requestParams, IEqualityComparer<string> comparer)
        {
            validator.ValidateAndThrow(requestParams);

            List<ExchangeRate> rates = CurrencyExchangeRateProviderFactory
                .Create(requestParams.CurrencyPublisher)
                .GetCurrencies();

            return rates
                .Where(r => requestParams.CurrencyCodes.Contains(r.Code, comparer))
                .ToList();
        }
    }
}
