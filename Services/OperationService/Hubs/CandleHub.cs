using DTO.BrokerRequests;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OperationService.Hubs
{
    public class CandleHub : Hub
    {
        public async Task Subscribe(GetCandlesRequest request)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,request.Figi);
        }
    }
}
