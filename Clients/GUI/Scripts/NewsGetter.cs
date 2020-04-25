using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;

namespace GUI.Scripts
{
    public static class NewsGetter
    {
        public static async Task<IEnumerable<NewsItem>> GetNews(string feedUrl)
        {
            const string url = "https://localhost:5007/news/getnews";

            var queryParams = new Dictionary<string, string>
            {
                { "feedUrl", feedUrl }
            };

            var client = new RestClient<string, IEnumerable<NewsItem>>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }
    }
}
