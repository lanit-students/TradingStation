using DTO;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RssIntegrationLib
{
    public class RamblerRssReader : INewsPublisher
    {
        public IEnumerable<NewsItem> GetNews(string feedUrl)
        {
            var feedXml = XDocument.Load(feedUrl);

            var news = from feed in feedXml.Descendants("item")
                       select new NewsItem
                       {
                           Title = feed.Element("title").Value,
                           Link = feed.Element("link").Value,
                           PubDate = feed.Element("pubDate").Value,
                           Description = feed.Element("description").Value,
                           Category = feed.Element("category").Value,
                           Author = feed.Element("author").Value
                       };

            return news;
        }
    }
}
