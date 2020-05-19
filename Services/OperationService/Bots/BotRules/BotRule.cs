using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using OperationService.Bots.Utils;
using System;
using System.Collections.Generic;

namespace OperationService.Bots.BotRules
{
    public class BotRule
    {
        private ICommand<TradeRequest, bool> tradeCommand;
        private ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand;

        private Guid userId;
        private string token;
        private OperationType operationType;

        private int timeMarker;
        private decimal triggerValue;

        public BotRule(
            CreateBotRuleRequest request,
            ICommand<TradeRequest, bool> tradeCommand,
            ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand)
        {
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
            userId = request.UserId;
            timeMarker = request.TimeMarker;
            triggerValue = request.TriggerValue;
            operationType = request.OperationType;
        }

        private void Execute(object sender, TriggerEventArgs e)
        {
            tradeCommand.Execute(
                new TradeRequest()
                {
                    UserId = userId,
                    Broker = BrokerType.TinkoffBroker,
                    Token = token,
                    Operation = operationType,
                    Figi = e.Figi,
                    Price = e.Price,
                    // TODO: add count logic
                    Count = 1,
                    Currency = e.Currency
                });
        }

        public void Start(List<string> figis, TradeRequest request)
        {
            token = request.Token;
            userId = request.UserId;

            foreach (var figi in figis)
            {
                var trigger = new TimeDifferenceTrigger(
                       figi,
                       timeMarker,
                       triggerValue,
                       token,
                       request.Currency,
                       candlesCommand
                    );

                trigger.Triggered += Execute;
            }
        }
    }
}
