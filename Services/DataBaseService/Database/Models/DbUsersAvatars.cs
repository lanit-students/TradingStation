using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseService.Database.Models
{
    public class DbUsersAvatars
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public byte[] Avatar { get; set; }
        public string AvatarExtension { get; set; }

        [ForeignKey("UserId")]
        public DbUser User { get; set; }
    }

    public class DbUserAvatarConfiguration : IEntityTypeConfiguration<DbUsersAvatars>
    {
        public void Configure(EntityTypeBuilder<DbUsersAvatars> builder)
        {
            builder
                .ToTable("UsersAvatars")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .HasIndex(p => p.Id)
                .IsUnique();
            builder
                .Property(p => p.Avatar)
                .HasColumnName("Avatar");
            builder
                .Property(p => p.AvatarExtension)
                .HasColumnName("AvatarExtension");
            builder
                .Property(p => p.UserId)
                .HasColumnName("UserId")
                .IsRequired();
        }
    }
}
