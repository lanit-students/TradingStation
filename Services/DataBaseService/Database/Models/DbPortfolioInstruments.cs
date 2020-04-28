using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace DataBaseService.Database.Models
{
    public class DbPortfolioInstruments
    {
        public Guid UserId { get; set; }
        public string Figi { get; set; }
        public string InstrumentType { get; set; }
        public int Lots { get; set; }
    }

    public class DbPortfolioInstrumentsConfiguration : IEntityTypeConfiguration<DbPortfolioInstruments>
    {
        public void Configure(EntityTypeBuilder<DbPortfolioInstruments> builder)
        {
            builder
                .ToTable("PortfolioInstruments")
                .HasKey(p => new { p.UserId, p.Figi });
            builder
                .Property(p => p.UserId)
                .HasColumnName("UserId")
                .IsRequired();
            builder
                .Property(p => p.Lots)
                .HasColumnName("Lots")
                .IsRequired();
            builder
                .Property(p => p.Figi)
                .HasColumnName("Figi")
                .IsRequired();
            builder
                .Property(p => p.InstrumentType)
                .HasColumnName("InstrumentType")
                .IsRequired();
        }
    }
}
