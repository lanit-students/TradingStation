using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class AddBotCommand : IAddBotCommand
    {
        private readonly IRequestClient<CreateBotRequest> client;

        public AddBotCommand([FromServices] IRequestClient<CreateBotRequest> client)
        {
            this.client = client;
        }

        public async Task<bool> Execute(CreateBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            var addBotResult = OperationResultHandler.HandleResponse(response.Message);

            if (!addBotResult)
            {
                throw new BadRequestException("Unable to create bot");
            }

            return addBotResult;
        }
    }
}
