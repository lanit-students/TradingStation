using Automatonymous;
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

        private Guid id;

        private OperationType type;

        private Trigger trigger;

        public BotRule([FromServices] ICommand<TradeRequest, bool> command)
        {
            this.command = command;
        }

        private void Execute(object sender, string e)
        {
            // do sth with figi (e is figi of instrument which got triggered)
        }

        public void Start(List<string> figis, TradeRequest request)
        {
            foreach (string figi in figis)
            {
                trigger = new PriceFiveMinutesBeforeTrigger();

                trigger.Triggered += Execute;
            }
        }
    }
}
