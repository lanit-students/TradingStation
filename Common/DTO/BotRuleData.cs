using DTO.MarketBrokerObjects;
using System;

namespace DTO
{
    public class BotRuleData
    {
        public OperationType OperationType { get; set; }
        public decimal MoneyLimitPercents { get; set; }
        public TimeSpan TimeMarker { get; set; }
        public decimal TriggerValue { get; set; }

        public override string ToString()
        {
            var s = OperationType == OperationType.Buy ? " less " : " more ";
            return OperationType.ToString() + " if price " + s + " than " + TriggerValue + " in limit "
                + MoneyLimitPercents + " % of balance" + " with interval " + TimeMarker.ToString();
        }
    }
}
