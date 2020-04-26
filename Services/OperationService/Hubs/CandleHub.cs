using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace OperationService.Hubs
{
    public class CandleHub : Hub
    {
        private readonly IRequestClient<SubscribeOnCandleRequest> client;

        public CandleHub([FromServices] IRequestClient<SubscribeOnCandleRequest> client)
        {
            this.client = client;
        }
        public async Task Subscribe(SubscribeOnCandleRequest request)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,request.Figi);
            await client.GetResponse<OperationResult>(request);

        }

        public async Task SendMessage(string Figi, Candle candle)
        {
            await Clients.Group(Figi).SendAsync("ReceiveMessage",JsonConvert.SerializeObject(candle));
        }
    }
}
