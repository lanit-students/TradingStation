using DTO;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class UpdateUserBalanceCommand: ICommand<UpdateUserBalanceRequest, bool>
    {
        private readonly IRequestClient<UserBalance> client;
        private readonly ILogger<UpdateUserBalanceCommand> logger;

        public UpdateUserBalanceCommand(
            [FromServices] IRequestClient<UserBalance> client,
            ILogger<UpdateUserBalanceCommand> logger
            )
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> UpdateUserBalance(UserBalance brokerUser)
        {
            var response = await client.GetResponse<OperationResult<bool>>(brokerUser);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(UpdateUserBalanceRequest request)
        {
            var brokerUser = new UserBalance()
            {
                UserId = request.UserId,
                BalanceInEur = request.BalanceInEur,
                BalanceInRub = request.BalanceInRub,
                BalanceInUsd = request.BalanceInUsd
            };
            var result = await UpdateUserBalance(brokerUser);
            logger.LogInformation($"Balance of user{request.UserId} updated successfully");
            return result;
        }
    }
}
