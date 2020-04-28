using System;
using System.Collections.Generic;

namespace DTO.MarketBrokerObjects
{
    public class BrokerUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public BrokerType Broker { get; set; }
        public decimal BalanceInRub { get; set; }
        public decimal BalanceInUsd { get; set; }
        public decimal BalanceInEur { get; set; }
    }
}
