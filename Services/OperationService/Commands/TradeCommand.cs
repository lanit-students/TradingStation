using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class TradeCommand : ICommand<TradeRequest, bool>
    {
        private readonly IRequestClient<InternalTradeRequest> client;
        //private readonly ILogger<TradeCommand> logger;

        public TradeCommand(
            [FromServices] IRequestClient<InternalTradeRequest> client)
        {
            this.client = client;
            //this.logger = logger;
        }

        private async Task<bool> Trade(InternalTradeRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(TradeRequest request)
        {
            var internalRequest = new InternalTradeRequest()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,             
                Broker = request.Broker,
                Token = request.Token,
                Operation = request.Operation,
                Figi = request.Figi,
                Lots = request.Lots,
                Price = request.Price,
                
                
            };
            return await Trade(internalRequest);
        }
    }
}
