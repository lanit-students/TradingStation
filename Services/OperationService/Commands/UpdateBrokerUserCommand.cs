using DTO;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class UpdateBrokerUserCommand: ICommand<UpdateUserBalanceRequest, bool>
    {
        private readonly IRequestClient<UserBalance> client;

        public UpdateBrokerUserCommand(
            [FromServices] IRequestClient<UserBalance> client
            )
        {
            this.client = client;
        }

        private async Task<bool> UpdateUser(UserBalance brokerUser)
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
            return await UpdateUser(brokerUser);
        }
    }
}
