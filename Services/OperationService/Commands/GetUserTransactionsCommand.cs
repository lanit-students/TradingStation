using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OperationService.Commands
{
    public class GetUserTransactionsCommand : ICommand<GetUserTransactionsRequest, IEnumerable<Transaction>>
    {
        private readonly IRequestClient<GetUserTransactions> client;
        private readonly ILogger<GetUserTransactionsCommand> logger;

        public GetUserTransactionsCommand(
            [FromServices] IRequestClient<GetUserTransactions> client,
            ILogger<GetUserTransactionsCommand> logger
        )
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<IEnumerable<Transaction>> GetUserTransactions(GetUserTransactions request)
        {
            var response = await client.GetResponse<OperationResult<IEnumerable<Transaction>>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<IEnumerable<Transaction>> Execute(GetUserTransactionsRequest request)
        {
            var getUserTransactions = new GetUserTransactions
            {
                UserId = request.UserId
            };

            var result = await GetUserTransactions(getUserTransactions);

            logger.LogInformation($"Transactions of user{request.UserId} taken successfully");

            return result;
        }
    }
}
