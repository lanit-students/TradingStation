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
        private decimal maxBalancePercents;

        private List<Trigger> triggers;

        public BotRule(
            StartBotRuleRequest request,
            ICommand<TradeRequest, bool> tradeCommand,
            ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand)
        {
            triggers = new List<Trigger>();
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
            userId = request.UserId;
            timeMarker = request.TimeMarker;
            triggerValue = request.TriggerValue;
            operationType = request.OperationType;
            maxBalancePercents = request.MoneyLimitPercents;
        }

        private void Execute(object sender, TriggerEventArgs e)
        {
            // TODO: get user's balance and apply max percents property
            var balance = 10000;

            tradeCommand.Execute(
                new TradeRequest()
                {
                    UserId = userId,
                    Broker = BrokerType.TinkoffBroker,
                    Token = token,
                    Operation = operationType,
                    Figi = e.Figi,
                    Price = e.Price,
                    Count = (int)(balance * maxBalancePercents / 100 / e.Price),
                    Currency = e.Currency
                });
        }

        public void Start(List<string> figis)
        {
            foreach (var figi in figis)
            {
                var trigger = new TimeDifferenceTrigger(
                       figi,
                       timeMarker,
                       triggerValue,
                       token,
                       candlesCommand
                    );

                trigger.Triggered += Execute;
                triggers.Add(trigger);
            }
        }

        public void Stop()
        {
            foreach (var trigger in triggers)
            {
                trigger.Disable();
            }
        }
    }
}
