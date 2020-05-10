using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotManager
    {
        public static async Task CreateBot(UserToken userToken, CreateBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/create";

            var client = new RestClient<CreateBotRequest, bool>(url, RestRequestType.POST, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task DeleteBot(UserToken userToken, DeleteBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/delete";

            var client = new RestClient<DeleteBotRequest, bool>(url, RestRequestType.DELETE, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task RunBot(UserToken userToken, RunBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/run";

            var client = new RestClient<RunBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task DisableBot(UserToken userToken, DisableBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/disable";

            var client = new RestClient<DisableBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task<List<BotInfoResponse>> GetBots(UserToken userToken)
        {
            const string url = "https://localhost:5009/operations/bot/get";

            var client = new RestClient<object, List<BotInfoResponse>>(url, RestRequestType.GET, userToken);

            return await client.ExecuteAsync();
        }
    }
}
