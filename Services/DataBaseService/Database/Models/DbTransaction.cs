using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public string Currency { get; set; }
        public bool IsSuccess { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
    }

    public class DbTransactionConfiguration : IEntityTypeConfiguration<DbTransaction>
    {
        public void Configure(EntityTypeBuilder<DbTransaction> builder)
        {
            builder
                .ToTable("Transactions")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.UserId)
                .HasColumnName("UserId")
                .IsRequired();
            builder
                .Property(p => p.Operation)
                .HasColumnName("Operation")
                .IsRequired();
            builder
                .Property(p => p.Price)
                .HasColumnName("Price")
                .IsRequired();
            builder
                .Property(p => p.Currency)
                .HasColumnName("Currency")
                .IsRequired();
            builder
                .Property(p => p.Figi)
                .HasColumnName("Figi")
                .IsRequired();
            builder
                .Property(p => p.Count)
                .HasColumnName("Count")
                .IsRequired();
            builder
                .Property(p => p.Broker)
                .HasColumnName("Broker")
                .IsRequired();
            builder
                .Property(p => p.Date)
                .HasColumnName("TransactionDate")
                .IsRequired();
            builder
                .Property(p => p.Time)
                .HasColumnName("TransactionTime")
                .IsRequired();
            builder
               .Property(p => p.IsSuccess)
               .HasColumnName("IsSuccess")
               .IsRequired();
            builder
                .HasIndex(p => p.Id)
                .IsUnique();
        }
    }
}
