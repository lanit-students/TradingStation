using DTO.MarketBrokerObjects;
using System;

namespace GUI.ViewModels
{
    public class BotRuleModel
    {
        public bool IsChoosen { get; set; }
        public OperationType OperationType { get; set; }
        public decimal MoneyLimitPercents { get; set; }
        public TimeSpan TimeMarker { get; set; }
        public decimal TriggerValue { get; set; }
        public string Description { get; set; }
    }
}
