using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.Database.Models
{
    public class DbUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class DbUserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();
            builder
                .Property(p => p.LastName)
                .HasColumnName("LastName")
                .IsRequired();
            builder
                .Property(p => p.Birthday)
                .HasColumnName("Birthday")
                .IsRequired();
            builder
                .HasIndex(p => p.Id)
                .IsUnique();
        }
    }
}
