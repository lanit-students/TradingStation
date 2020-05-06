using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotRunner
    {
        public static async Task Run(UserToken userToken, RunBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/run";

            var client = new RestClient<RunBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task Disable(UserToken userToken, DisableBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/disable";

            var client = new RestClient<DisableBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
