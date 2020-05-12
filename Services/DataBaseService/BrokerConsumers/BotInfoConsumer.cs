using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class BotInfoConsumer : IConsumer<InternalGetBotsRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly ILogger logger;

        public BotInfoConsumer([FromServices] IBotRepository botRepository, [FromServices] ILogger<BotInfoConsumer> logger)
        {
            this.botRepository = botRepository;
            this.logger = logger;
        }

        private List<BotData> BotInfo(InternalGetBotsRequest request)
        {
            logger.LogInformation("Get bots request received from OperationService");
            return botRepository.GetBots(request);
        }

        public async Task Consume(ConsumeContext<InternalGetBotsRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(BotInfo, context.Message);

            await context.RespondAsync(response);
        }
    }
}
