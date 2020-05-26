using DTO.MarketBrokerObjects;
using System;

namespace DTO.RestRequests
{
    public class TradeRequest
    {
        public Guid UserId { get; set; }
        public BrokerType Broker { get; set; }
        public string Token { get; set; }
        public OperationType Operation { get; set; }
        public string Figi { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
    }
}
