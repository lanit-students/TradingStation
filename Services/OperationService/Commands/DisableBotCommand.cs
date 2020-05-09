using DTO;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class DisableBotCommand : ICommand<DisableBotRequest, bool>
    {
        private readonly IRequestClient<DisableBotRequest> client;

        public DisableBotCommand([FromServices] IRequestClient<DisableBotRequest> client)
        {
            this.client = client;
        }

        private async Task<bool> DisableBot(DisableBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(DisableBotRequest request)
        {
            var disableBotresult = await DisableBot(request);
            if (!disableBotresult)
            {
                throw new BadRequestException("Unable to disable bot");
            }

            return disableBotresult;
        }
    }
}
