using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using FluentValidation;
using DTO.NewsRequests.Currency;
using DTO;
using NewsService.Interfaces;

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
        public List<ExchangeRate> GetCurrencies(
            [FromServices] IGetCurrenciesCommand command,
            [FromServices] IEqualityComparer<string> comparer,
            [FromBody] CurrencyRequest requestParams)
        {
            return command.Execute(requestParams, comparer);
        }

        [Route("getnews")]
        [HttpGet]
        public IEnumerable<NewsItem> GetNews([FromServices] IGetNewsCommand command, [FromQuery] string feedUrl)
        {
            return command.Execute(feedUrl);
        }
    }
}

