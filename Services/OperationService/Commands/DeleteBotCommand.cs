﻿using DTO;
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

        private async Task<bool> DeleteBot(DeleteBotRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(DeleteBotRequest request)
        {
            var deleteBotResult = await DeleteBot(request);

            if (!deleteBotResult)
            {
                throw new BadRequestException("Unable to delete bot");
            }

            return deleteBotResult;
        }
    }
}
