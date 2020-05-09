using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.Database.Models
{
    public class DbPortfolio
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Figi { get; set; }
        public string Broker { get; set; }
        public int Count { get; set; }
    }

    public class DbPortfolioInstrumentsConfiguration : IEntityTypeConfiguration<DbPortfolio>
    {
        public void Configure(EntityTypeBuilder<DbPortfolio> builder)
        {
            builder
                .ToTable("Portfolios")
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
                .Property(p => p.Count)
                .HasColumnName("Count")
                .IsRequired();
            builder
                .Property(p => p.Figi)
                .HasColumnName("Figi")
                .IsRequired();
            builder
                .Property(p => p.Broker)
                .HasColumnName("Broker")
                .IsRequired();
        }
    }
}
