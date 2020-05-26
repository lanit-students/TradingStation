using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class GetBotsCommand : ICommand<Guid, List<BotData>>
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

        private async Task<List<BotData>> GetBotsByUserId(InternalGetBotsRequest request)
        {
            var response = await client.GetResponse<OperationResult<List<BotData>>>(request);
            logger.LogInformation("Response from Database Service GetBotsByUserId method received");
            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<List<BotData>> Execute(Guid request)
        {
            try
            {
                var internalRequest = new InternalGetBotsRequest { UserId = request };

                return await GetBotsByUserId(internalRequest);
            }
            catch(Exception)
            {
                var e = new NotFoundException("Not found user to get bots");
                logger.LogWarning(e, $"{e.Message}, userId: {request}");
                throw e;
            }
        }
    }
}
