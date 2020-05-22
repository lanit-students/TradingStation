using System;
﻿using DTO.MarketBrokerObjects;

namespace DTO
{
    public class BotRuleData
    {
        public Guid Id { get; set; }

        public Guid BotId { get; set; }

        public OperationType OperationType { get; set; }

        public int MoneyLimitPercents { get; set; }

        public int TimeMarker { get; set; }

        public decimal TriggerValue { get; set; }

        public override string ToString()
        {
            var s = OperationType == OperationType.Buy ? " less " : " more ";
            return OperationType.ToString() + " if price " + s + " than " + TriggerValue + " in limit "
                + MoneyLimitPercents + " % of balance" + " with interval " + TimeMarker.ToString();
        }
    }
}
