using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OperationService.Interfaces;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class DisableBotCommand: IDisableBotCommand
    {
        private readonly IRequestClient<DisableBotRequest> client;

        public DisableBotCommand([FromServices] IRequestClient<DisableBotRequest> client)
        {
            this.client = client;
        }

        public async Task<bool> Execute(DisableBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            var run = OperationResultHandler.HandleResponse(response.Message);

            if (!run)
            {
                throw new BadRequestException("Unable to disable bot");
            }

            return run;
        }
    }
}
