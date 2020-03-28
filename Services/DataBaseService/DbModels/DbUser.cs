using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.DbModels
{
    public class DbUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Email").IsRequired().HasMaxLength(30);
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired().HasMaxLength(30);
            builder.Property(p => p.Password).HasColumnName("Password").IsRequired().HasMaxLength(25);
        }
    }
}
