using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Collections.Generic;

using FluentValidation;

using DTO.NewsRequests;
using DTO.NewsRequests.Currency;
using NewsService.Utils;

namespace NewsService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    /// <summary>
    /// To verify NewsService work in the url - field add "/NewsService/get?newsPublisherType=CentralBank"
    /// You can select only NewsService to start
    /// </summary>

    public class NewsServiceController : ControllerBase
    {
        [Route("info")]
        [HttpPost]

        ///<summary>
        /// Return news depending on a publisher type
        /// </summary>
        public List<ExchangeRate> GetCurrencies(
            [FromServices] IValidator<CurrencyRequest> validator,
            [FromBody] CurrencyRequest requestParams)
        {
            validator.ValidateAndThrow(requestParams);
            List<ExchangeRate> rates = NewsPublisherFactory
                .Create(requestParams.CurrecyPublisher)
                .GetCurrencies();

            return rates
                .Where(r => requestParams.CurrencyCodes.Contains(r.Code))
                .ToList();
        }
    }
}

