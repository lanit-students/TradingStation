using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using OperationService.Commands;

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
