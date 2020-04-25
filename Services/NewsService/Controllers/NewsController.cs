using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using FluentValidation;
using DTO.NewsRequests.Currency;
using DTO;
using NewsService.Interfaces;
using DTO.RestRequests;

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
            [FromServices] IValidator<CurrencyRequest> validator,
            [FromServices] IEqualityComparer<string> comparer,
            [FromBody] CurrencyRequest requestParams)
        {
            validator.ValidateAndThrow(requestParams);

            return command.Execute(requestParams, comparer);
        }

        [Route("getnews")]
        [HttpPost]
        public IEnumerable<NewsItem> GetNews([FromServices] IGetNewsCommand command, [FromBody] GetNewsRequest request)
        {
            return command.Execute(request.FeedUrl);
        }
    }
}

