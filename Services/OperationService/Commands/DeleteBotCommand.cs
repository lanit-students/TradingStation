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
    public class DeleteBotCommand : ICommand<DeleteBotRequest, bool>
    {
        private readonly IRequestClient<DeleteBotRequest> client;

        public DeleteBotCommand([FromServices] IRequestClient<DeleteBotRequest> client)
        {
            this.client = client;
        }

        public async Task<bool> Execute(DeleteBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            var delete = OperationResultHandler.HandleResponse(response.Message);

            if (!delete)
            {
                throw new BadRequestException("Unable to create bot");
            }

            return delete;
        }
    }
}
