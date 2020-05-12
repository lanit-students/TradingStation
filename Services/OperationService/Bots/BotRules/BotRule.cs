using DTO.MarketBrokerObjects;
using OperationService.Bots.Utils;
using System;
using System.Collections.Generic;

namespace OperationService.Bots.BotRules
{
    public class BotRule
    {
        public Guid Id { get; set; }

        private OperationType Type { get; set; }

        private Trigger Trigger { get; set; }

        public void Execute(List<string> figis)
        {
            foreach (string figi in figis)
            {
                if (!Trigger.Check(figi))
                    continue;
            }
        }
    }
}
