using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Kernel.CustomExceptions;
using OperationService.Bots.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.Bots.BotRules
{
    public class BotRule
    {
        private ICommand<TradeRequest, bool> tradeCommand;
        private ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand;
        private ICommand<GetUserBalanceRequest, UserBalance> balanceCommand;

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
            ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand,
            ICommand<GetUserBalanceRequest, UserBalance> balanceCommand)
        {
            triggers = new List<Trigger>();
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
            this.balanceCommand = balanceCommand;
            userId = request.UserId;
            timeMarker = request.TimeMarker;
            triggerValue = request.TriggerValue;
            operationType = request.OperationType;
            maxBalancePercents = request.MoneyLimitPercents;
        }

        private async Task Execute(object sender, TriggerEventArgs e)
        {
            var userBalance = await balanceCommand.Execute(
                new GetUserBalanceRequest()
                {
                    UserId = userId
                });

            var balance = e.Currency switch
            {
                Currency.Rub => userBalance.BalanceInRub,
                Currency.Eur => userBalance.BalanceInEur,
                Currency.Usd => userBalance.BalanceInUsd,
                _ => throw new BadRequestException("Unsupported currency")
            };

            var maxPrice = balance * maxBalancePercents / 100;

            await tradeCommand.Execute(
                new TradeRequest()
                {
                    UserId = userId,
                    Broker = BrokerType.TinkoffBroker,
                    Token = token,
                    Operation = operationType,
                    Figi = e.Figi,
                    Price = e.Price,
                    Count = (int)(maxPrice / e.Price),
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

                trigger.Triggered += async (s, e) =>
                {
                    await Execute(s, e);
                };

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
