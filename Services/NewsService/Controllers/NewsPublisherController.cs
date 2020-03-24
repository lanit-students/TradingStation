using Microsoft.AspNetCore.Mvc;
using System;
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
        [Route("getNews")]
        [HttpGet]

        ///<summary>
        /// Return news depending on a publisher type
        /// </summary>
        public String GetNews([FromQuery] NewsPublisherTypes newsPublisherType)
        {
            return  NewsPublisherFactory.Create(newsPublisherType).GetNews();
        }
    }
}

