using System.Collections.Generic;
using DTO;

namespace Interfaces
{
    /// <summary>
    /// An interface for each news publisher
    /// </summary>
    public interface INewsPublisher
    {
        IEnumerable<NewsItem> GetNews(string feedUrl);
    }
}
