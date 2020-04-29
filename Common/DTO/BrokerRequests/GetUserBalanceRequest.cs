using DTO.MarketBrokerObjects;
using System;

namespace DTO.BrokerRequests
{
    public class GetUserBalanceRequest
    {
        public Guid UserId { get; set; }
        public BrokerType Broker { get; set; }
    }
}
