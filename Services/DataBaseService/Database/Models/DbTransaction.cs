using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseService.Database.Models
{
    public class DbTransaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Broker { get; set; }
        public string Operation { get; set; }
        public string Figi { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        //public bool IsSuccess { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime TransactionTime { get; set; }
    }
}
