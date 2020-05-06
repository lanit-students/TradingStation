using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotGetter
    {
        public static async Task<List<BotInfoRequest>> GetBots(UserToken userToken)
        {
            const string url = "https://localhost:5009/operations/bot/get";

            var client = new RestClient<object, List<BotInfoRequest>>(url, RestRequestType.GET, userToken);

            return await client.ExecuteAsync();
        }
    }
}
