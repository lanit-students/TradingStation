using DTO;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OperationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class AddBotCommand : IAddBotCommand
    {
        private readonly IRequestClient<CreateBotRequest> client;
        private readonly IValidator<CreateBotRequest> validator;

        public AddBotCommand([FromServices] IRequestClient<CreateBotRequest> client, [FromServices] IValidator<CreateBotRequest> validator)
        {
            this.client = client;
            this.validator = validator;
        }

        public async Task<bool> Execute(CreateBotRequest request)
        {
            validator.ValidateAndThrow(request);

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
