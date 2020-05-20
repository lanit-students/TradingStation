using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OperationService.Bots;
using OperationService.Bots.BotRules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;

namespace OperationService.Commands
{
    public class RunBotCommand : ICommand<RunBotRequest, bool>
    {
        private readonly IRequestClient<RunBotRequest> client;
        private readonly ILogger<RunBotCommand> logger;
        private ICommand<TradeRequest, bool> tradeCommand;
        private ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand;

        public RunBotCommand(
            [FromServices] IRequestClient<RunBotRequest> client,
            [FromServices] ILogger<RunBotCommand> logger,
            [FromServices] ICommand<TradeRequest, bool> tradeCommand,
            [FromServices] ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand)
        {
            this.client = client;
            this.logger = logger;
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
        }

        private async Task RunBot(RunBotRequest request)
        {
            logger.LogInformation("Response from Database Service RunBot method received");

            var response = await client.GetResponse<OperationResult<List<BotRuleData>>>(request);

            var rulesData = OperationResultHandler.HandleResponse(response.Message);

            BotRunner.Run(rulesData, request.Figis, tradeCommand, candlesCommand);
        }

        public async Task<bool> Execute(RunBotRequest request)
        {
            try
            {
                await RunBot(request);
                return true;
            }
            catch (Exception)
            {
                var e = new NotFoundException("Not found bot to run");
                logger.LogWarning(e, $"{e.Message}, botId: {request.Id}");
                throw e;
            }
        }
    }
}
