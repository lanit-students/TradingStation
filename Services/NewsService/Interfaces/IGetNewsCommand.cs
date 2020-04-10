using DTO;
using System.Collections.Generic;

namespace NewsService.Interfaces
{
    public interface IGetNewsCommand
    {
        IEnumerable<NewsItem> Execute(string request);
    }
}
