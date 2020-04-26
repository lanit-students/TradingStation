using DTO;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class TradeCommand : ICommand<TradeRequest, bool>
    {
        private readonly IRequestClient<TradeRequest> client;
        private readonly ILogger<TradeCommand> logger;

        public TradeCommand(
            [FromServices] IRequestClient<TradeRequest> client,
            [FromServices] ILogger<TradeCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> Trade(TradeRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(TradeRequest request)
        {
            return await Trade(request);
        }
    }
}
