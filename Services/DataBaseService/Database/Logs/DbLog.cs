using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataBaseService.Database.Models
{
    public class DbLog
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public string ServiceName { get; set; }
    }

    public class DbLogsConfiguration : IEntityTypeConfiguration<DbLog>
    {
        public void Configure(EntityTypeBuilder<DbLog> builder)
        {
            builder
                .ToTable("_Logs")
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder
                .Property(p => p.ParentId)
                .HasColumnName("ParentId")
                .IsRequired();
            builder
                .Property(p => p.Type)
                .HasColumnName("Type")
                .IsRequired();
            builder
                .Property(p => p.Time)
                .HasColumnName("Time")
                .IsRequired();
            builder
                .Property(p => p.Message)
                .HasColumnName("Message")
                .IsRequired();
            builder
                .Property(p => p.ServiceName)
                .HasColumnName("ServiceName")
                .IsRequired();
        }
    }
}
