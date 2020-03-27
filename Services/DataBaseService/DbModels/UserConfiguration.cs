using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.DbModels
{
    public class UserConfigure : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.ToTable("Mobiles").HasKey(p => p.Id);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(30);
        }
    }
}
