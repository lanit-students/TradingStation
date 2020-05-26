using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Kernel.CustomExceptions;
using MassTransit.Initializers;
using OperationService.Bots.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationService.Bots.BotRules
{
    public class BotRule
    {
        private ICommand<TradeRequest, bool> tradeCommand;
        private ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand;
        private ICommand<GetUserBalanceRequest, UserBalance> balanceCommand;
        private ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> instrumentsCommand;

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
            ICommand<GetUserBalanceRequest, UserBalance> balanceCommand,
            ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> instrumentsCommand)
        {
            triggers = new List<Trigger>();
            this.tradeCommand = tradeCommand;
            this.candlesCommand = candlesCommand;
            this.balanceCommand = balanceCommand;
            this.instrumentsCommand = instrumentsCommand;
            token = request.Token;
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

            if (maxPrice < e.Price)
            {
                return;
            }

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
            var instruments = instrumentsCommand.Execute(
                new GetInstrumentsRequest()
                {
                    Broker = BrokerType.TinkoffBroker,
                    Token = token,
                    Type = InstrumentType.Any
                }).Result;

            foreach (var figi in figis)
            {
                try
                {
                    var currency = instruments.Where(x => x.Figi == figi).Select(x => x.Currency).First();

                    var trigger = new TimeDifferenceTrigger(
                       figi,
                       timeMarker,
                       triggerValue,
                       token,
                       currency,
                       candlesCommand
                    );

                    trigger.Triggered += async (s, e) =>
                    {
                        try
                        {
                            await Execute(s, e);
                        }
                        catch { }
                    };

                    triggers.Add(trigger);
                }
                catch
                {
                    throw new BadRequestException();
                }
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
