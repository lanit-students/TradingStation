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

        private Guid Id { get; set; }

        private OperationType Type { get; set; }

        private Trigger Trigger { get; set; }

        public BotRule([FromServices] ICommand<TradeRequest, bool> command)
        {
            this.command = command;
        }

        public void Execute(List<string> figis, TradeRequest request)
        {
            foreach (string figi in figis)
            {
                if (!Trigger.Check(figi))
                    continue;
                else
                {
                    request.Figi = figi;
                    command.Execute(request);
                }
            }
        }
    }
}
