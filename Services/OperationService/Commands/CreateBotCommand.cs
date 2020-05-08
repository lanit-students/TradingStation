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
    public class CreateBotCommand : ICommand<CreateBotRequest, bool>
    {
        private readonly IRequestClient<CreateBotRequest> client;

        public CreateBotCommand([FromServices] IRequestClient<CreateBotRequest> client)
        {
            this.client = client;
        }

        public async Task<bool> Execute(CreateBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            var create = OperationResultHandler.HandleResponse(response.Message);

            if (!create)
            {
                throw new BadRequestException("Unable to create bot");
            }

            return create;
        }
    }
}
