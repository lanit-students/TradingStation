using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseService.Database.Models
{
    public class DbUserCredential
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public DbUser User { get; set; }
    }

    public class DbUserCredentialConfiguration : IEntityTypeConfiguration<DbUserCredential>
    {
        public void Configure(EntityTypeBuilder<DbUserCredential> builder)
        {
            builder
                .ToTable("UsersCredentials")
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
                .Property(p => p.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(p => p.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired();
            builder
                .HasIndex(p => p.Id)
                .IsUnique();
            builder
                .HasIndex(p => p.Email)
                .IsUnique()
                .IsClustered();
            builder
               .Property(p => p.IsActive)
               .HasColumnName("IsActive")
               .HasDefaultValue(0);
        }
    }
}
