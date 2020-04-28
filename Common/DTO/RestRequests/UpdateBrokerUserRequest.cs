using DTO.MarketBrokerObjects;
using System;

namespace DTO.RestRequests
{
    public class UpdateBrokerUserRequest
    {
        public Guid UserId { get; set; }
        public BrokerType Broker { get; set; }
        public decimal BalanceInRub { get; set; }
        public decimal BalanceInUsd { get; set; }
        public decimal BalanceInEur { get; set; }
    }
}
