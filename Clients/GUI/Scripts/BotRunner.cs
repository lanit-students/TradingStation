using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotRunner
    {
        public static async Task Run(UserToken userToken, EditBotRequest request)
        {
            const string url = "https://localhost:5009/operations/runbot";

            var client = new RestClient<EditBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task Disable(UserToken userToken, EditBotRequest request)
        {
            const string url = "https://localhost:5009/operations/runbot";

            var client = new RestClient<EditBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
