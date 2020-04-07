using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseService.Database.Models
{
    public class DbUserCredential : IEquatable<DbUserCredential>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public DbUser User { get; set; }

        public bool Equals([AllowNull] DbUserCredential other)
        {
            if (other == null)
                return false;

            if (Id == other.Id
                && UserId == other.UserId
                && Email == other.Email
                && PasswordHash == other.PasswordHash
                && IsActive == other.IsActive
                && User == other.User)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            DbUser dbUserObj = obj as DbUser;
            if (dbUserObj == null)
                return false;
            else
                return Equals(dbUserObj);
        }

        public override int GetHashCode()
        {
            return (Id.ToString()
                + UserId.ToString()
                + Email
                + PasswordHash
                + IsActive.ToString()
                + User.GetHashCode().ToString())
                .GetHashCode();
        }
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
