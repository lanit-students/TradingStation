using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DataBaseService.Database.Models
{
    public class DbUser : IEquatable<DbUser>
    {
        public static bool operator ==(DbUser first, DbUser second)
        {
            if (((object)first) == null
                || ((object)second == null))
                return Object.Equals(first, second);

            return first.Equals(second);
        }

        public static bool operator !=(DbUser first, DbUser second)
        {
            if (((object)first) == null
                || ((object)second) == null)
                return !Object.Equals(first, second);

            return !first.Equals(second);
        }

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
        }
    }
}
