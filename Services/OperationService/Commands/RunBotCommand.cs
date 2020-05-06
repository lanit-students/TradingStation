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
    public class RunBotCommand: IRunBotCommand
    {
        private readonly IRequestClient<RunBotRequest> client;

        public RunBotCommand([FromServices] IRequestClient<RunBotRequest> client)
        {
            this.client = client;
        }

        public async Task<bool> Execute(RunBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            var run = OperationResultHandler.HandleResponse(response.Message);

            if (!run)
            {
                throw new BadRequestException("Unable to run bot");
            }

            return run;
        }
    }
}
