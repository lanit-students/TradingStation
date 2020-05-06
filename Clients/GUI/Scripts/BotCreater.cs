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
            const string url = "https://localhost:5009/operations/createbot";

            var client = new RestClient<CreateBotRequest, bool>(url, RestRequestType.POST, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task Delete(UserToken userToken, EditBotRequest request)
        {
            const string url = "https://localhost:5009/operations/deletebot";

            var client = new RestClient<EditBotRequest, bool>(url, RestRequestType.DELETE, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
