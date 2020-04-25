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
        public static async Task<IEnumerable<NewsItem>> GetNews(GetNewsRequest newsRequest)
        {
            const string url = "https://localhost:5007/news/getnews";

            var client = new RestClient<GetNewsRequest, IEnumerable<NewsItem>>(url, RestRequestType.POST);

            return await client.Execute(newsRequest);
        }
    }
}
