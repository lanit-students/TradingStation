using DTO.MarketBrokerObjects;
using System;

namespace DTO.RestRequests
{
    public class CreateBotRuleRequest
    {
        public Guid UserId { get; set; }
        public Guid BotId { get; set; }
        public string Token { get; set; }
        public BrokerType Broker { get; set; }
        public int TimeMarker { get; set; }
        public int TriggerValue { get; set; }
        public OperationType OperationType { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
