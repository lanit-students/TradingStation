using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class CreateBotConsumer : IConsumer<CreateBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly IBotRuleRepository ruleRepository;
        private readonly ILogger logger;

        public CreateBotConsumer(
            [FromServices] IBotRepository botRepository,
            [FromServices] IBotRuleRepository ruleRepository,
            [FromServices] ILogger<CreateBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.ruleRepository = ruleRepository;
            this.logger = logger;
        }

        private bool CreateBot(CreateBotRequest request)
        {
            logger.LogInformation("Create bot request received from OperationService");

            var botId = Guid.NewGuid();

            var bot = new BotData()
            {
                Name = request.Name,
                Id = botId,
                UserId = request.UserId,
                IsRunning = false
            };

            botRepository.CreateBot(bot);

            foreach (var rule in request.Rules)
            {
                rule.Id = Guid.NewGuid();
                rule.BotId = botId;
                ruleRepository.SaveRuleForBot(rule);
            }

            return true;
        }

        public async Task Consume(ConsumeContext<CreateBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
