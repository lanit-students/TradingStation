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
    public class UpdateBrokerUserCommand: ICommand<UpdateBrokerUserRequest, bool>
    {
        private readonly IRequestClient<BrokerUser> client;

        public UpdateBrokerUserCommand(
            [FromServices] IRequestClient<BrokerUser> client
            )
        {
            this.client = client;
        }

        private async Task<bool> UpdateUser(BrokerUser brokerUser)
        {
            var response = await client.GetResponse<OperationResult<bool>>(brokerUser);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(UpdateBrokerUserRequest request)
        {
            var brokerUser = new BrokerUser()
            {
                UserId = request.UserId,
                Broker = request.Broker,
                BalanceInEur = request.BalanceInEur,
                BalanceInRub = request.BalanceInRub,
                BalanceInUsd = request.BalanceInUsd
            };
            return await UpdateUser(brokerUser);
        }
    }
}
