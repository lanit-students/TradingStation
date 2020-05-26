using DataBaseService.Repositories.Interfaces;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class EditBotConsumer : IConsumer<EditBotRequest>
    {
        private readonly IBotRepository botRepository;
        private readonly IBotRuleRepository botRuleRepository;
        private readonly ILogger<EditBotConsumer> logger;

        public EditBotConsumer([FromServices] IBotRepository botRepository, [FromServices] IBotRuleRepository botRuleRepository, [FromServices]ILogger<EditBotConsumer> logger)
        {
            this.botRepository = botRepository;
            this.botRuleRepository = botRuleRepository;
            this.logger = logger;
        }

        private bool EditBot(EditBotRequest request)
        {
            logger.LogInformation("EditBot request received from Service");
            botRepository.EditBot(request);
            foreach (var rule in request.Rules)
            {
                botRuleRepository.EditRuleForBot(rule, request.BotId);
            }
            return true;
        }

        public async Task Consume(ConsumeContext<EditBotRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(EditBot, context.Message);

            await context.RespondAsync(response);
        }
    }
}
