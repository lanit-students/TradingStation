using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Database.Models
{
    public class DbTinkoffUser
    {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
        public Guid PortfolioId { get; set; }
		public decimal BalanceInRub { get; set; }
		public decimal BalanceInUsd { get; set; }
		public decimal BalanceInEur { get; set; }
    }

    public class DbTinkoffUserConfiguration : IEntityTypeConfiguration<DbTinkoffUser>
    {
        public void Configure(EntityTypeBuilder<DbTinkoffUser> builder)
        {
            builder
                .ToTable("TinkoffUsers")
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
                .Property(p => p.PortfolioId)
                .HasColumnName("PortfolioId")
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
