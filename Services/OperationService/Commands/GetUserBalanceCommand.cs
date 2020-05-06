using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class GetUserBalanceCommand: ICommand<GetUserBalanceRequest, UserBalance>
    {
        private readonly IRequestClient<GetUserBalanceRequest> client;
        private readonly ILogger<GetUserBalanceCommand> logger;

        public GetUserBalanceCommand(
            [FromServices] IRequestClient<GetUserBalanceRequest> client,
            [FromServices] ILogger<GetUserBalanceCommand> logger
            )
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<UserBalance> GetUser(GetUserBalanceRequest request)
        {
            var response = await client.GetResponse<OperationResult<UserBalance>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<UserBalance> Execute(GetUserBalanceRequest request)
        {
            var result = await GetUser(request);
            logger.LogInformation($"Balance of user {request.UserId} received successfully");
            return result;
        }
    }
}
