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
        public static async Task<List<BotsInfoRequest>> GetBots(UserToken userToken)
        {
            const string url = "https://localhost:5011/operations/getbots";

            var client = new RestClient<object, List<BotsInfoRequest>>(url, RestRequestType.GET, userToken);

            return await client.ExecuteAsync();
        }
    }
}
