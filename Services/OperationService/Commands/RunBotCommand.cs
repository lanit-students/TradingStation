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
    public class RunBotCommand : ICommand<RunBotRequest, bool>
    {
        private readonly IRequestClient<RunBotRequest> client;

        public RunBotCommand([FromServices] IRequestClient<RunBotRequest> client)
        {
            this.client = client;
        }

        private async Task<bool> RunBot(RunBotRequest request)
        {
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
                throw new NotFoundException("bot not found");
            }
        }
    }
}
