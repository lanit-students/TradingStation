using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class RunBotConsumer : IConsumer<RunBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly IBotRuleRepository botRuleRepository;
        private readonly ILogger logger;

        public RunBotConsumer(
            [FromServices] IBotRepository botRepository,
            [FromServices] IBotRuleRepository botRuleRepository,
            [FromServices] ILogger<RunBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.botRuleRepository = botRuleRepository;
            this.logger = logger;
        }

        private List<BotRuleData> RunBot(RunBotRequest request)
        {
            logger.LogInformation("Run bot request received from OperationService");
            botRepository.RunBot(request.Id);

            var rules = botRuleRepository.GetBotRules(request.Id);

            return rules;
        }

        public async Task Consume(ConsumeContext<RunBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(RunBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
