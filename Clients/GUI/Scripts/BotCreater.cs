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
            const string url = "https://localhost:5011/users/addbot";

            var client = new RestClient<CreateBotRequest, bool>(url, RestRequestType.POST, userToken);

            await client.ExecuteAsync(request);
        }
    }
}
