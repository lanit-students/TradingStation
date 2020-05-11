using DTO.MarketBrokerObjects;
using OperationService.Bots.Utils;
using System;
using System.Collections.Generic;

namespace OperationService.Bots.BotRules
{
    public class BotRule
    {
        public Guid Id { get; set; }

        private OperationType type { get; set; }

        private Trigger trigger { get; set; }

        public void Execute(List<string> figis)
        {
            foreach (string figi in figis)
            {
                if (!trigger.Check(figi))
                    continue;
            }
        }
    }
}
