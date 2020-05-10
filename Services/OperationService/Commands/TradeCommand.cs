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
        private readonly IRequestClient<InternalTradeRequest> tradeClient;
        private readonly IRequestClient<Transaction> saveTransactionClient;
        private readonly ILogger<TradeCommand> logger;

        public TradeCommand(
            [FromServices] IRequestClient<InternalTradeRequest> tradeClient,
            [FromServices] IRequestClient<Transaction> saveTransactionClient,
            [FromServices] ILogger<TradeCommand> logger
            )
        {
            this.tradeClient = tradeClient;
            this.saveTransactionClient = saveTransactionClient;
            this.logger = logger;
        }

        private async Task<Transaction> Trade(InternalTradeRequest request)
        {
            var response = await tradeClient.GetResponse<OperationResult<Transaction>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        private async Task<bool> SaveTransaction(Transaction transaction)
        {
            var response = await saveTransactionClient.GetResponse<OperationResult<bool>>(transaction);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(TradeRequest request)
        {
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Broker = request.Broker,
                UserId = request.UserId,
                Operation = request.Operation,
                Figi = request.Figi,
                Count = request.Count,
                Price = request.Price,
                Currency = request.Currency
            };
            var tradeRequest = new InternalTradeRequest()
            {
                Token = request.Token,
                Transaction = transaction
            };

            try
            {
                transaction = await Trade(tradeRequest);
                await SaveTransaction(transaction);
            }
            catch (BadRequestException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new BadRequestException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }

            logger.LogInformation($"Transaction of user {request.UserId} finished successfully");
            return transaction.IsSuccess;
        }
    }
}
