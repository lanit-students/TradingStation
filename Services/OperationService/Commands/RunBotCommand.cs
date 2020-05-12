using DTO;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class RunBotCommand : ICommand<RunBotRequest, bool>
    {
        private readonly IRequestClient<RunBotRequest> client;
        private readonly ILogger<RunBotCommand> logger;

        public RunBotCommand([FromServices] IRequestClient<RunBotRequest> client, [FromServices] ILogger<RunBotCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> RunBot(RunBotRequest request)
        {
            logger.LogInformation("Response from Database Service RunBot method received");

            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(RunBotRequest request)
        {
            try
            {
                return await RunBot(request);
            }
            catch (Exception)
            {
                var e = new NotFoundException("Not found bot to run");
                logger.LogWarning(e, $"{e.Message}, botId: {request.ID}");
                throw e;
            }
        }
    }
}
