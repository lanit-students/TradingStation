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
    public class GetBrokerUserCommand: ICommand<GetBrokerUserRequest, BrokerUser>
    {
        private readonly IRequestClient<GetBrokerUserRequest> client;

        public GetBrokerUserCommand(
            [FromServices] IRequestClient<GetBrokerUserRequest> client
            )
        {
            this.client = client;
        }

        private async Task<BrokerUser> GetUser(GetBrokerUserRequest request)
        {
            var response = await client.GetResponse<OperationResult<BrokerUser>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<BrokerUser> Execute(GetBrokerUserRequest request)
        {
            return await GetUser(request);
        }
    }
}
