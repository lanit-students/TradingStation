using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class SaveBotRuleCommand
    {
        public class SaveRuleCommand : ICommand<BotRule, Instrument>
        {
            private readonly IRequestClient<GetInstrumentFromPortfolioRequest> client;
            private readonly ILogger<GetInstrumentFromPortfolioCommand> logger;

            public SaveRuleCommand(
                [FromServices] IRequestClient<GetInstrumentFromPortfolioRequest> client,
                [FromServices] ILogger<GetInstrumentFromPortfolioCommand> logger
                )
            {
                this.client = client;
                this.logger = logger;
            }

            private async Task<bool> SaveRule(BotRule rule)
            {
                var response = await client.GetResponse<OperationResult<bool>>(rule);

                return OperationResultHandler.HandleResponse(response.Message);
            }

            public async Task<bool> Execute(BotRule rule)
            {
                var result = await SaveRule(rule);
                logger.LogInformation($"Bot rule {rule.Id} save successfully");
                return result;
            }
        }
}
