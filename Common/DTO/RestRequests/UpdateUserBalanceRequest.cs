using System;

namespace DTO.RestRequests
{
    public class UpdateUserBalanceRequest
    {
        public Guid UserId { get; set; }
        public decimal BalanceInRub { get; set; }
        public decimal BalanceInUsd { get; set; }
        public decimal BalanceInEur { get; set; }
    }
}
