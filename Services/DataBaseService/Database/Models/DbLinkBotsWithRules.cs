using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Database.Models
{
    public class DbLinkBotsWithRules
    {
        public Guid Id { get; set; }

        public Guid IdBot { get; set; }

        public Guid IdRule { get; set; }
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
                .Property(p => p.IdBot)
                .HasColumnName("IdBot")
                .IsRequired();
            builder
                .Property(p => p.IdRule)
                .HasColumnName("IdRule")
                .IsRequired();
        }
    }
}
