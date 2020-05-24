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
        private readonly IRequestClient<RunBotRequest> runClient;
        private readonly IRequestClient<InternalGetBotRulesRequest> getRulesClient;
        private readonly ILogger<RunBotCommand> logger;
        private ICommand<TradeRequest, bool> tradeCommand;
        private ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand;
        private ICommand<GetUserBalanceRequest, UserBalance> balanceCommand;
        private ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> instrumentsCommand;

        public RunBotCommand(
            [FromServices] IRequestClient<RunBotRequest> runClient,
            [FromServices] IRequestClient<InternalGetBotRulesRequest> getRulesClient,
            [FromServices] ILogger<RunBotCommand> logger,
            [FromServices] ICommand<TradeRequest, bool> tradeCommand,
            [FromServices] ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand,
            [FromServices] ICommand<GetUserBalanceRequest, UserBalance> balanceCommand,
            [FromServices] ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> instrumentsCommand)
        {

            this.runClient = runClient;
            this.getRulesClient = getRulesClient;
            this.logger = logger;
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
            this.balanceCommand = balanceCommand;
            this.instrumentsCommand = instrumentsCommand;
        }

        private async Task RunBot(InternalGetBotRulesRequest getRulesRequest, RunBotRequest runRequest)
        {
            logger.LogInformation("Response from Database Service RunBot method received");

            var response = await getRulesClient.GetResponse<OperationResult<List<BotRuleData>>>(getRulesRequest);

            var rulesData = OperationResultHandler.HandleResponse(response.Message);

            BotRunner.Run(runRequest, rulesData, tradeCommand, candlesCommand, balanceCommand, instrumentsCommand);

            await runClient.GetResponse<OperationResult<bool>>(runRequest);
        }

        public async Task<bool> Execute(RunBotRequest runRequest)
        {
            var getRulesRequest = new InternalGetBotRulesRequest() { BotId = runRequest.BotId };
            try
            {
                await RunBot(getRulesRequest, runRequest);
                return true;
            }
            catch (NotFoundException)
            {
                var e = new NotFoundException("Not found bot to run");
                logger.LogWarning(e, $"{e.Message}, botId: {runRequest.BotId}");
                throw e;
            }
            catch (BadRequestException e)
            {
                logger.LogWarning(e, $"{e.Message}, botId: {runRequest.BotId}");
                throw e;
            }
        }
    }
}
