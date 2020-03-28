using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using DTO;
using NewsService.Utils;

namespace NewsService.Controllers
{
    /// <summary>
    /// To verify NewsService work in the url - field add "/NewsService/get?newsPublisherType=CentralBank"
    /// You can select only NewsService to start
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        ///<summary>
        /// Return news depending on a publisher type
        /// </summary>
        [Route("currencies")]
        [HttpGet]
        public List<ExchangeRate> GetCurrencies([FromQuery] NewsPublisherTypes newsPublisherType)
        {
            return  NewsPublisherFactory.Create(newsPublisherType).GetCurrencies();
        }
    }
}
