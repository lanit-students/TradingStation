using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace DataBaseService.Database.Models
{
    public class DbBot
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
    }

    public class DbBotConfiguration : IEntityTypeConfiguration<DbBot>
    {
        public void Configure(EntityTypeBuilder<DbBot> builder)
        {
            throw new NotImplementedException();
        }
    }
}
