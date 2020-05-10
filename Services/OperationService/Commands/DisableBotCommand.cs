using DTO;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                return await DisableBot(request);
            }
            catch (Exception)
            {
                throw new NotFoundException("bot not found");
            }
        }
    }
}
