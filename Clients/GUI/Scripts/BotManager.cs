using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class BotManager
    {
        public static async Task CreateBot(UserToken userToken, CreateBotRequest request)
        {
            const string url = "http://localhost:5008/operations/bot/create";

            var client = new RestClient<CreateBotRequest, bool>(url, RestRequestType.POST, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task DeleteBot(UserToken userToken, DeleteBotRequest request)
        {
            const string url = "http://localhost:5008/operations/bot/delete";

            var client = new RestClient<DeleteBotRequest, bool>(url, RestRequestType.DELETE, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task RunBot(UserToken userToken, RunBotRequest request)
        {
            const string url = "http://localhost:5008/operations/bot/run";

            var client = new RestClient<RunBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task DisableBot(UserToken userToken, DisableBotRequest request)
        {
            const string url = "http://localhost:5008/operations/bot/disable";

            var client = new RestClient<DisableBotRequest, bool>(url, RestRequestType.PUT, userToken);

            await client.ExecuteAsync(request);
        }

        public static async Task<List<BotData>> GetBots(UserToken userToken)
        {
            const string url = "http://localhost:5008/operations/bot/get";

            var client = new RestClient<object, List<BotData>>(url, RestRequestType.GET, userToken);

            return await client.ExecuteAsync();
        }

        public static async Task<BotData> GetBot(UserToken userToken, Guid botId)
        {
            throw new NotImplementedException();
        }

        public static async Task EditBot(UserToken userToken, EditBotRequest request)
        {

        }
    }
}
