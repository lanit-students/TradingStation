using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OperationService.Bots;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class RunBotCommand : ICommand<RunBotRequest, bool>
    {
        private readonly IRequestClient<RunBotRequest> client;
        private readonly ILogger<RunBotCommand> logger;
        private ICommand<TradeRequest, bool> tradeCommand;
        private ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand;
        private ICommand<GetUserBalanceRequest, UserBalance> balanceCommand;

        public RunBotCommand(
            [FromServices] IRequestClient<RunBotRequest> client,
            [FromServices] ILogger<RunBotCommand> logger,
            [FromServices] ICommand<TradeRequest, bool> tradeCommand,
            [FromServices] ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand,
            [FromServices] ICommand<GetUserBalanceRequest, UserBalance> balanceCommand)
        {
            this.client = client;
            this.logger = logger;
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
            this.balanceCommand = balanceCommand;
        }

        private async Task RunBot(RunBotRequest request)
        {
            logger.LogInformation("Response from Database Service RunBot method received");

            var response = await client.GetResponse<OperationResult<List<BotRuleData>>>(request);

            var rulesData = OperationResultHandler.HandleResponse(response.Message);

            BotRunner.Run(request, rulesData, tradeCommand, candlesCommand, balanceCommand);
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
                logger.LogWarning(e, $"{e.Message}, botId: {request.BotId}");
                throw e;
            }
        }
    }
}
