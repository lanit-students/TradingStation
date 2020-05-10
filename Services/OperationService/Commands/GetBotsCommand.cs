using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class GetBotsCommand : ICommand<Guid, List<BotInfoResponse>>
    {
        private readonly IRequestClient<InternalGetBotsRequest> client;
        private readonly ILogger<GetBotsCommand> logger;

        public GetBotsCommand
            ([FromServices]IRequestClient<InternalGetBotsRequest> client,
            [FromServices] ILogger<GetBotsCommand> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        private async Task<List<BotInfoResponse>> GetBotsByUserId(InternalGetBotsRequest request)
        {
            logger.LogInformation("Response from Database Service GetBotsByUserId method received");
            var response = await client.GetResponse<OperationResult<List<BotInfoResponse>>>(request);
            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<List<BotInfoResponse>> Execute(Guid request)
        {
            var internalRequest = new InternalGetBotsRequest { UserId = request };

            return await GetBotsByUserId(internalRequest);
        }
    }
}
