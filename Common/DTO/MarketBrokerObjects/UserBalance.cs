using System;

namespace DTO.MarketBrokerObjects
{
    public class UserBalance : ICloneable
    {
        public Guid UserId { get; set; }
        public decimal BalanceInRub { get; set; }
        public decimal BalanceInUsd { get; set; }
        public decimal BalanceInEur { get; set; }

        public object Clone()
        {
            return new UserBalance()
            {
                UserId = UserId,
                BalanceInRub = BalanceInRub,
                BalanceInUsd = BalanceInUsd,
                BalanceInEur = BalanceInEur
            };
        }
    }
}
