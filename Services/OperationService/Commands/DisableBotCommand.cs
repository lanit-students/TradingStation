using DTO;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OperationService.Bots;
using System;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class DisableBotCommand : ICommand<DisableBotRequest, bool>
    {
        private readonly IRequestClient<DisableBotRequest> client;
        private readonly ILogger<DisableBotCommand> logger;

        public DisableBotCommand([FromServices] IRequestClient<DisableBotRequest> client, [FromServices] ILogger<DisableBotCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> DisableBot(DisableBotRequest request)
        {
            logger.LogInformation("Response from Database Service DisableBot method received");

            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(DisableBotRequest request)
        {
            try
            {
                BotRunner.Stop(request.ID);

                await DisableBot(request);

                return true;
            }
            catch (Exception)
            {
                var e = new NotFoundException("Not found bot to disable");
                logger.LogWarning(e, $"{e.Message}, botId: {request.ID}");
                throw e;
            }
        }
    }
}
