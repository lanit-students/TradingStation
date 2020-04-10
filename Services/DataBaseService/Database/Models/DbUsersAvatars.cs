using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseService.Database.Models
{
    public class DbUsersAvatars
    {
        public Guid Id { get; set; }
        public byte[] Avatar { get; set; }
        public string TypeAvatar { get; set; }
    }

    public class DbUserAvatarConfiguration : IEntityTypeConfiguration<DbUsersAvatars>
    {
        public void Configure(EntityTypeBuilder<DbUsersAvatars> builder)
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
