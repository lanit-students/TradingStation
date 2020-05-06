using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotRunner
    {
        public static async Task Run(RunBotRequest request, UserToken userToken)
        {
            const string url = "https://localhost:5011/operations/runbot";

            var client = new RestClient<RunBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task Disable(DisableBotRequest request, UserToken userToken)
        {
            const string url = "https://localhost:5011/operations/runbot";

            var client = new RestClient<DisableBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
