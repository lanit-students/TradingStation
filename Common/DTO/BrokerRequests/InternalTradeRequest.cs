
using DTO.MarketBrokerObjects;
using System;

namespace DTO.BrokerRequests
{
    public class InternalTradeRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public BrokerType Broker { get; set; }
        public string Token { get; set; }
        public OperationType Operation { get; set; }
        public string Figi { get; set; }
        public int Lots { get; set; }
        public decimal Price { get; set; }
    }
}
