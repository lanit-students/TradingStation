using System;
using System.Collections.Generic;

namespace DTO.MarketBrokerObjects
{
    public class UserBalance
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal BalanceInRub { get; set; }
        public decimal BalanceInUsd { get; set; }
        public decimal BalanceInEur { get; set; }
    }
}
