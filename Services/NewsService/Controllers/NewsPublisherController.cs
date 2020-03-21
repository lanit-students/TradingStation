using Microsoft.AspNetCore.Mvc;
using System;
using NewsService.Utils;

namespace NewsService.Controllers
{
    [ApiController]

    /// <summary>
    /// To verify NewsService work in the url - field add "/NewsService/get?newsPublisherType=CentralBank"
    /// You can select only NewsService to start
    /// </summary>
    
    public class NewsServiceController : ControllerBase
    {
        [Route("[controller]/getNews")]
        [HttpGet]

        ///<summary>
        /// Return news depends on a publisher type
        /// </summary>
        public String GetNews([FromQuery] NewsPublisherTypes newsPublisherType)
        {
            return  NewsPublisherFactory.CreateNewsPublisher(newsPublisherType).GetNews();
        }
    }
}

