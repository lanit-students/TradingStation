using DTO;
using NewsService.Interfaces;
using NewsService.Utils;
using System;
using System.Collections.Generic;
namespace NewsService.Commands
{
    public class GetNewsCommand : IGetNewsCommand
    {
        public IEnumerable<NewsItem> Execute(string request)
        {
            if (request.Contains("finance.rambler.ru/rss"))
            {
                return NewsPublisherFactory.Create(Enums.NewsPublisherTypes.Rambler).GetNews(request);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
