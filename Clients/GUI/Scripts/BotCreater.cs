using DTO.Bots;
using DTO.RestRequests;
using Kernel;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotCreater
    {
        public static void Create(Bot bot, Guid userId)
        {
            var createBotRequest = new CreateBotRequest(bot, userId);
            var url = "";
            var client = new RestClient<CreateBotRequest, Task<bool>>(url, Kernel.Enums.RestRequestType.POST); 
        }
    }
}
