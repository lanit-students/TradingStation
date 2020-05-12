using DataBaseService.Repositories.Interfaces;
using DTO;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class SaveRuleConsumerConsumer : IConsumer<BotRule>
    {
        private readonly IBotRuleRepository botRuleRepository;

        public SaveRuleConsumerConsumer([FromServices] IBotRuleRepository botRuleRepository)
        {
            this.botRuleRepository = botRuleRepository;
        }

        private bool SaveRule(BotRule rule)
        {
            botRuleRepository.SaveRuleForBot(rule);
            return true;
        }

        public async Task Consume(ConsumeContext<BotRule> context)
        {
            var response = OperationResultWrapper.CreateResponse(SaveRule, context.Message);

            await context.RespondAsync(response);
        }
    }
}
