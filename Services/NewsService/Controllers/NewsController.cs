using Microsoft.AspNetCore.Mvc;

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

    public class NewsController : ControllerBase
    {
        ///<summary>
        /// Return news depending on a publisher type
        /// </summary>
        [Route("currencies")]
        [HttpPost]
        public string GetCurrencies(
            //[FromBody] CurrencyRequest requestParams,
            [FromServices] IValidator<CurrencyRequest> validator)
        {
            CurrencyRequest requestParams = new CurrencyRequest
            {
                CurrencyCodes = new List<string> { "eur", "usd" },
                CurrecyPublisher = NewsPublisherTypes.CentralBank
            };

            validator.ValidateAndThrow(requestParams);
            List<ExchangeRate> rates = NewsPublisherFactory
                .Create(requestParams.CurrecyPublisher)
                .GetCurrencies();

            return "hhh";
                //rates
                //.Where(r => requestParams.CurrencyCodes.Contains(r.Code))
                //.ToList();
        }
    }
}

