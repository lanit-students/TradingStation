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
    public class CreateBotCommand : ICommand<CreateBotRequest, bool>
    {
        private readonly IRequestClient<CreateBotRequest> client;
        private readonly ILogger<CreateBotCommand> logger;

        public CreateBotCommand([FromServices] IRequestClient<CreateBotRequest> client, [FromServices] ILogger<CreateBotCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> CreateBot(CreateBotRequest request)
        {
            logger.LogInformation("Response from Database Service CreateBot method received");

            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(CreateBotRequest request)
        {
            try
            {
                return await CreateBot(request);
            }
            catch (Exception)
            {
                var e = new BadRequestException("Unable to create bot");
                logger.LogWarning(e, $"{e.Message}");
                throw e;
            }
        }
    }
}
