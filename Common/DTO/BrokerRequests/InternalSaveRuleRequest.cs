using DTO.MarketBrokerObjects;
using System;

namespace DTO.BrokerRequests
{
    public class InternalSaveRuleRequest
    {
        public Guid Id { get; set; }
        public OperationType Type { get; set; }
        public int TimeMarker { get; set; }
        public int TriggerValue { get; set; }
    }
}
