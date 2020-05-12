using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Database.Models
{
    public class DbBotRule
    {
        public Guid Id { get; set; }

        public int OperationType { get; set; }

        public int TimeMarker { get; set; }

        public int TriggerValue { get; set; }

        public class DbPortfolioInstrumentsConfiguration : IEntityTypeConfiguration<DbBotRule>
        {
            public void Configure(EntityTypeBuilder<DbBotRule> builder)
            {
                builder
                    .ToTable("BotRules")
                    .HasKey(p => p.Id);
                builder
                    .Property(p => p.Id)
                    .HasColumnName("Id")
                    .IsRequired();
                builder
                    .Property(p => p.OperationType)
                    .HasColumnName("OperationType")
                    .IsRequired();
                builder
                    .Property(p => p.TimeMarker)
                    .HasColumnName("TimeMarker")
                    .IsRequired();
                builder
                    .Property(p => p.TriggerValue)
                    .HasColumnName("TriggerValue")
                    .IsRequired();
            }
        }
    }
}
