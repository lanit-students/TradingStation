﻿using DTO.MarketBrokerObjects;
using System;

namespace DTO
{
    public class BotRuleData
    {
        public OperationType OperationType { get; set; }
        public decimal MoneyLimitPercents { get; set; }
        public TimeSpan TimeMarker { get; set; }
        public decimal TriggerValue { get; set; }
    }
}
