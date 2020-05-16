using DataBaseService.Repositories.Interfaces;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class SaveBotRuleConsumer : IConsumer<InternalSaveRuleRequest>
    {
        private readonly IBotRuleRepository botRuleRepository;

        public SaveBotRuleConsumer([FromServices] IBotRuleRepository botRuleRepository)
        {
            this.botRuleRepository = botRuleRepository;
        }

        private bool SaveRule(InternalSaveRuleRequest request)
        {
            botRuleRepository.SaveRuleForBot(request.Rule);
            return true;
        }

        public async Task Consume(ConsumeContext<InternalSaveRuleRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(SaveRule, context.Message);

            await context.RespondAsync(response);
        }
    }
}
