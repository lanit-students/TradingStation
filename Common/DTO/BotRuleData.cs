using DTO.MarketBrokerObjects;
using System;

namespace DTO
{
    public class BotRuleData
    {
        public OperationType OperationType { get; set; }
        public decimal MoneyLimitPercents { get; set; }
        public DateTime TimeMarker { get; set; }
        public decimal TriggerValue { get; set; }
        public string Description { get; set; }
    }
}
