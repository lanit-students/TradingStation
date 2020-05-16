using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class SaveBotRuleCommand : ICommand<InternalSaveRuleRequest, bool>
    {
        private readonly IRequestClient<InternalSaveRuleRequest> client;
        private readonly ILogger<InternalSaveRuleRequest> logger;

        public SaveBotRuleCommand(
            [FromServices] IRequestClient<InternalSaveRuleRequest> client,
            [FromServices] ILogger<InternalSaveRuleRequest> logger
            )
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> SaveRule(InternalSaveRuleRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(InternalSaveRuleRequest request)
        {
            var result = await SaveRule(request);
            logger.LogInformation($"Bot rule {request.Rule.Id} save successfully");
            return result;
        }
    }
}
