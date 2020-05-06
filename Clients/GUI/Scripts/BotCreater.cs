using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotCreater
    {
        public static async Task Create(UserToken userToken, CreateBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/create";

            var client = new RestClient<CreateBotRequest, bool>(url, RestRequestType.POST, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task Delete(UserToken userToken, DeleteBotRequest request)
        {
            const string url = "https://localhost:5009/operations/bot/delete";

            var client = new RestClient<DeleteBotRequest, bool>(url, RestRequestType.DELETE, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
