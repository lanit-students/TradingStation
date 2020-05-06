using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class DisableBotConsumer : IConsumer<DisableBotRequest>
    {
        private readonly IBotRepository botRepository;

        public DisableBotConsumer([FromServices] IBotRepository botRepository)
        {
            this.botRepository = botRepository;
        }

        private bool CreateBot(DisableBotRequest request)
        {
            return true;
        }

        public async Task Consume(ConsumeContext<DisableBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
