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
    public class DeleteBotCommand : ICommand<DeleteBotRequest, bool>
    {
        private readonly IRequestClient<DeleteBotRequest> client;
        private readonly ILogger<DeleteBotCommand> logger;

        public DeleteBotCommand([FromServices] IRequestClient<DeleteBotRequest> client, [FromServices] ILogger<DeleteBotCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> DeleteBot(DeleteBotRequest request)
        {
            logger.LogInformation("Response from Database Service DeleteBot method received");

            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(DeleteBotRequest request)
        {
            try
            {
                if (BotRunner.IsRunning(request.ID))
                {
                    BotRunner.Stop(request.ID);
                }

                return await DeleteBot(request);
            }
            catch (Exception)
            {
                var e = new NotFoundException("Not found bot to delete");
                logger.LogWarning(e, $"{e.Message}, botId: {request.ID}");
                throw e;
            }
        }
    }
}
