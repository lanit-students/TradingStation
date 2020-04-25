using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace OperationService.Hubs
{
    public class CandleHub : Hub
    {
        public async Task Subscribe(string Figi)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Figi);
        }

        public async Task SendMessage(string Figi, Candle candle)
        {
            await Clients.Group(Figi).SendAsync("ReceiveMessage",JsonConvert.SerializeObject(candle));
        }
    }
}
