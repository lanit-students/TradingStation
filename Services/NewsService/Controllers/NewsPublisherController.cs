using Microsoft.AspNetCore.Mvc;
using System;
using Interfaces;
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
        [Route("[controller]/get")]
        [HttpGet]
        public String GetNews([FromQuery] NewsPublisherTypes newsPublisherType)
        {
            INewsPublisher newsPublisher = NewsPublisherFactory.CreateNewsPublisher(newsPublisherType);
            return newsPublisher.GetNews();
        }
    }
}

