using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.DbModels
{
    public class DbUserCredential
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<DbUserCredential>
    {
        public void Configure(EntityTypeBuilder<DbUserCredential> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(30);
            builder
                .Property(p => p.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
