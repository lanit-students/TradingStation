using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.Database.Models
{
    public class DbUserBalance
    {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public decimal BalanceInRub { get; set; }
		public decimal BalanceInUsd { get; set; }
		public decimal BalanceInEur { get; set; }
    }

    public class DbTinkoffUserConfiguration : IEntityTypeConfiguration<DbUserBalance>
    {
        public void Configure(EntityTypeBuilder<DbUserBalance> builder)
        {
            builder
                .ToTable("UserBalances")
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
                .Property(p => p.BalanceInRub)
                .HasColumnName("BalanceInRub")
                .IsRequired();
            builder
                .Property(p => p.BalanceInUsd)
                .HasColumnName("BalanceInUsd")
                .IsRequired();
            builder
                .Property(p => p.BalanceInEur)
                .HasColumnName("BalanceInEur")
                .IsRequired();
            builder
                .HasIndex(p => p.Id)
                .IsUnique();
        }
    }
}
