using System;
using System.Collections.Generic;

namespace DTO.MarketBrokerObjects
{
    public class BrokerUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Dictionary<Currency, decimal> Balance { get; set; }
        public BrokerType Broker { get; set; }
    }
}
