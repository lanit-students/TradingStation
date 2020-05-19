using DTO.RestRequests;
using OperationService.Bots.BotRules;
using System;
using System.Collections.Generic;

namespace OperationService.Bots
{
    public class BotRunner
    {
        public Guid UserId { get; set; }

        public List<BotRule> Rules { get; set; }

        public void Run(List<string> figis, TradeRequest request)
        {
            foreach (var rule in Rules)
            {
                rule.Start(figis, request);
            }
        }
        public void Stop() { }
    }
}
