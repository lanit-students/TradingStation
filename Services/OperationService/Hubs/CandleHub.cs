using DTO.BrokerRequests;
using Microsoft.AspNetCore.SignalR;

namespace OperationService.Hubs
{
    public class CandleHub : Hub
    {
        public async void Subscribe(GetCandlesRequest request)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,request.Figi);
        }
    }
}
