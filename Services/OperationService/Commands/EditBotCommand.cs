using DTO;
using DTO.BrokerRequests;
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
    public class EditBotCommand : ICommand<EditBotRequest, bool>
    {
        private readonly IRequestClient<EditBotRequest> client;
        private readonly ILogger<EditBotRequest> logger;

        public EditBotCommand([FromServices] IRequestClient<EditBotRequest> client, [FromServices] ILogger<EditBotRequest> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<bool> EditBot(EditBotRequest request)
        {
            logger.LogInformation("Response from Database Service EditBot method received");

            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(EditBotRequest request)
        {
            try
            {
                return await EditBot(request);
            }
            catch (Exception)
            {
                var e = new BadRequestException("Unable to edit bot");
                logger.LogWarning(e, $"{e.Message}");
                throw e;
            }
        }
    }
}
