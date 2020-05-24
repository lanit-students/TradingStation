using DataBaseService.Repositories.Interfaces;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class RunBotConsumer : IConsumer<RunBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly ILogger logger;

        public RunBotConsumer(
            [FromServices] IBotRepository botRepository,
            [FromServices] ILogger<RunBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.logger = logger;
        }

        private bool RunBot(RunBotRequest request)
        {
            logger.LogInformation("Run bot request received from OperationService");
            botRepository.RunBot(request.BotId);

            return true;
        }

        public async Task Consume(ConsumeContext<RunBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(RunBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
