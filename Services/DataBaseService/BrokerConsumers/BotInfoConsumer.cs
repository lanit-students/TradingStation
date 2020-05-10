using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class BotInfoConsumer : IConsumer<InternalGetBotsRequest>
    {
        private readonly IBotRepository botRepository;

        public BotInfoConsumer([FromServices] IBotRepository botRepository)
        {
            this.botRepository = botRepository;
        }

        private List<BotData> BotInfo(InternalGetBotsRequest request)
        {
            return botRepository.GetBots(request);
        }

        public async Task Consume(ConsumeContext<InternalGetBotsRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(BotInfo, context.Message);

            await context.RespondAsync(response);
        }
    }
}
