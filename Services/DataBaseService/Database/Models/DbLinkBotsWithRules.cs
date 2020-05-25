using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.Database.Models
{
    public class DbLinkBotsWithRules
    {
        public Guid Id { get; set; }

        public Guid BotId { get; set; }

        public Guid RuleId { get; set; }
    }

    public class DbLinkBotsWithRulesConfiguration : IEntityTypeConfiguration<DbLinkBotsWithRules>
    {
        public void Configure(EntityTypeBuilder<DbLinkBotsWithRules> builder)
        {
            builder
                .ToTable("LinkBotsWithRules")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.BotId)
                .HasColumnName("IdBot")
                .IsRequired();
            builder
                .Property(p => p.RuleId)
                .HasColumnName("IdRule")
                .IsRequired();
        }
    }
}
