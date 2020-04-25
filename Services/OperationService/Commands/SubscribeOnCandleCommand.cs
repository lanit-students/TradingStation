using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace OperationService.Commands
{
    public class SubscribeOnCandleCommand : ICommand<SubscribeOnCandleRequest, OperationResult>
    {
        private readonly IRequestClient<SubscribeOnCandleRequest> client;
        public SubscribeOnCandleCommand([FromServices] IRequestClient<SubscribeOnCandleRequest> client)
        {
            this.client = client;
        }

        private async Task<OperationResult> SubscribeOnCandle(SubscribeOnCandleRequest request)
        {
            var response = await client.GetResponse<OperationResult>(request);

            return response.Message;
        }

        public Task<OperationResult> Execute(SubscribeOnCandleRequest request)
        {
            return SubscribeOnCandle(request);
        }
    }
}
