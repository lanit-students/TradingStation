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
    public class GetBotRulesConsumer: IConsumer<InternalGetBotRulesRequest>
    {
        private readonly IBotRuleRepository botRuleRepository;
        private readonly ILogger logger;

        public GetBotRulesConsumer(
            [FromServices] IBotRuleRepository botRuleRepository,
            [FromServices] ILogger<RunBotConsumer> logger)
        {
            this.botRuleRepository = botRuleRepository;
            this.logger = logger;
        }

        private List<BotRuleData> GetRules(InternalGetBotRulesRequest request)
        {
            logger.LogInformation("Get bot rules request received from OperationService");

            var rules = botRuleRepository.GetBotRules(request.BotId);

            return rules;
        }

        public async Task Consume(ConsumeContext<InternalGetBotRulesRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetRules, context.Message);

            await context.RespondAsync(response);
        }
    }
}
