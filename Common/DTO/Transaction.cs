using DTO.MarketBrokerObjects;
using System;

namespace DTO
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public BrokerType Broker { get; set; }
        public OperationType Operation { get; set; }
        public string Figi { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public bool IsSuccess { get; set; }
    }
}
