using DTO;
using Kernel.CustomExceptions;
using NewsService.Interfaces;
using NewsService.Utils;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace NewsService.Commands
{
    public class GetNewsCommand : IGetNewsCommand
    {
        public IEnumerable<NewsItem> Execute(string request)
        {
            if (request.Contains("finance.rambler.ru/rss"))
            {
                try
                {
                    return NewsPublisherFactory.Create(Enums.NewsPublisherTypes.Rambler).GetNews(request);
                }
                catch (XmlException)
                {
                    throw new BadRequestException("Invalid rss feed url.");
                }
                catch (WebException)
                {
                    throw new NotFoundException("Rss feed not found.");
                }
            }
            else
            {
                throw new BadRequestException("Invalid news publisher url.");
            }
        }
    }
}
