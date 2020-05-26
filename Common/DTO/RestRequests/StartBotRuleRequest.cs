using DTO.MarketBrokerObjects;
using System;

namespace DTO.RestRequests
{
    public class StartBotRuleRequest
    {
        public Guid UserId { get; set; }
        public Guid BotId { get; set; }
        public string Token { get; set; }
        public BrokerType Broker { get; set; }
        public int TimeMarker { get; set; }
        public decimal TriggerValue { get; set; }
        public OperationType OperationType { get; set; }
        public decimal MoneyLimitPercents { get; set; }
    }
}
