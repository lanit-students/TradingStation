using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class BotInfoConsumer : IConsumer<BotInfoRequest>
    {
        private readonly IBotRepository botRepository;

        public BotInfoConsumer([FromServices] IBotRepository botRepository)
        {
            this.botRepository = botRepository;
        }

        private bool BotInfo(BotInfoRequest request)
        {
            
            return true;
        }

        public async Task Consume(ConsumeContext<BotInfoRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(BotInfo, context.Message);

            await context.RespondAsync(response);
        }
    }
}
