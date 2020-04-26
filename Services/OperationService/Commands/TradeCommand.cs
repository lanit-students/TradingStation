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

        private async Task<bool> SaveTransaction(InternaTransactionRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(TradeRequest request)
        {
            var tradeRequest = new InternalTradeRequest()
            {
                UserId = request.UserId,             
                Broker = request.Broker,
                Token = request.Token,
                Operation = request.Operation,
                Figi = request.Figi,
                Lots = request.Lots,
                Price = request.Price
            };
            var tradeResult = await Trade(tradeRequest);
            bool saveTransactionResult = false;
            if (tradeResult == true)
            {
                var transactionRequest = new InternaTransactionRequest()
                {
                    Id = Guid.NewGuid(),
                    Trade = tradeRequest
                };
                saveTransactionResult = await SaveTransaction(transactionRequest);
            }
                
            return saveTransactionResult;
        }
    }
}
