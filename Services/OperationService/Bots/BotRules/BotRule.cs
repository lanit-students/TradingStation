using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using OperationService.Bots.Utils;
using System;
using System.Collections.Generic;

namespace OperationService.Bots.BotRules
{
    public class BotRule
    {
        private ICommand<TradeRequest, bool> command;

        private string token;
        private OperationType operationType;
        private decimal moneyLimit;

        private int timeMarker;
        private decimal triggerValue;

        public BotRule(
            string token,
            int timeInterval,
            decimal priceDifference,
            OperationType type,
            decimal transactionMoneyLimit,
            [FromServices] ICommand<TradeRequest, bool> command)
        {
            this.command = command;
            this.token = token;
            timeMarker = timeInterval;
            triggerValue = priceDifference;
            operationType = type;
            moneyLimit = transactionMoneyLimit;
        }

        private void Execute(object sender, TriggerEventArgs e)
        {
            command.Execute(
                new TradeRequest()
                {
                    UserId = Guid.Empty,
                    Broker = BrokerType.TinkoffBroker,
                    Token = token,
                    Operation = operationType,
                    Figi = e.Figi,
                    Price = e.Price,
                    Count = (int)(moneyLimit / e.Price),
                    Currency = e.Currency
                });
        }

        public void Start(List<string> figis, TradeRequest request)
        {
            foreach (string figi in figis)
            {
                //var trigger = new TimeDifferenceTrigger();
                //
                //trigger.Triggered += Execute;
            }
        }
    }
}
