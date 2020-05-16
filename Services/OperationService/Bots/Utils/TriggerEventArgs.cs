using DTO.MarketBrokerObjects;
using System;

namespace OperationService.Bots.Utils
{
    public class TriggerEventArgs : EventArgs
    {
        public string Figi { get; set; }

        public decimal Price { get; set; }

        public Currency Currency { get; set; }
    }
}
