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
                throw new BadRequestException("unable to create bot");
            }
        }
    }
}
