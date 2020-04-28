using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class GetBrokerUserCommand: ICommand<GetUserBalanceRequest, UserBalance>
    {
        private readonly IRequestClient<GetUserBalanceRequest> client;

        public GetBrokerUserCommand(
            [FromServices] IRequestClient<GetUserBalanceRequest> client
            )
        {
            this.client = client;
        }

        private async Task<UserBalance> GetUser(GetUserBalanceRequest request)
        {
            var response = await client.GetResponse<OperationResult<UserBalance>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<UserBalance> Execute(GetUserBalanceRequest request)
        {
            return await GetUser(request);
        }
    }
}
