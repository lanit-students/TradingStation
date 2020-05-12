using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class DisableBotConsumer : IConsumer<DisableBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly ILogger logger;

        public DisableBotConsumer([FromServices] IBotRepository botRepository, [FromServices] ILogger<DisableBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.logger = logger;
        }

        private bool DisableBot(DisableBotRequest request)
        {
            logger.LogInformation("Disable bot request received from OperationService");
            botRepository.StopBot(request.ID);
            return true;
        }

        public async Task Consume(ConsumeContext<DisableBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(DisableBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
