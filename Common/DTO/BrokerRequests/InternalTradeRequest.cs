
using DTO.MarketBrokerObjects;
using System;

namespace DTO.BrokerRequests
{
    public class InternalTradeRequest
    {
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public BrokerType Broker { get; set; }
        public string Token { get; set; }
        public OperationType Operation { get; set; }
        public string Figi { get; set; }
        public int Lots { get; set; }
        public decimal Price { get; set; }
        public bool IsSuccess { get; set; }
    }
}
