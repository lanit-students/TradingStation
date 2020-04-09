using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseService.Database.Models
{
    public class DbUserAvatar
    {
        public Guid Id { get; set; }
        public byte[] Avatar { get; set; }
        public string TypeAvatar { get; set; }
    }

    public class DbUserAvatarConfiguration : IEntityTypeConfiguration<DbUserAvatar>
    {
        public void Configure(EntityTypeBuilder<DbUserAvatar> builder)
        {
            builder
                .ToTable("UserAvatars")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.Avatar)
                .HasColumnName("Avatar")
                .IsRequired();
            builder
                .Property(p => p.TypeAvatar)
                .HasColumnName("TypeAvatar")
                .IsRequired();
        }
    }
}
