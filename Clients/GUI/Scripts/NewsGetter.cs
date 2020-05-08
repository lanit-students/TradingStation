using System.Collections.Generic;
using DTO;
using Kernel;
using Kernel.Enums;

namespace GUI.Scripts
{
    public static class NewsGetter
    {
        public static IEnumerable<NewsItem> GetNews(string feedUrl)
        {
            const string url = "https://localhost:5007/news/getnews";

            var queryParams = new Dictionary<string, string>
            {
                { "feedUrl", feedUrl }
            };

            var client = new RestClient<string, IEnumerable<NewsItem>>(url, RestRequestType.GET, queryParams: queryParams);

            return client.Execute();
        }
    }
}
