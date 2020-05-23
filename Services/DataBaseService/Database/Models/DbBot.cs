using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.Database.Models
{
    public class DbBot
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public bool IsRunning { get; set; }
    }

    public class DbBotConfiguration : IEntityTypeConfiguration<DbBot>
    {
        public void Configure(EntityTypeBuilder<DbBot> builder)
        {
            builder
                .ToTable("Bots")
                .HasKey("Id");
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.UserId)
                .HasColumnName("UserId")
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();
            builder
                .Property(p => p.IsRunning)
                .HasColumnName("IsRunning")
                .IsRequired();
            builder
                .HasIndex(p => p.Id)
                .IsUnique();
        }
    }
}
