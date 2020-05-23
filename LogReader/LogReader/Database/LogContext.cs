using Kernel;
using Microsoft.EntityFrameworkCore;

namespace LogReader.Database
{
    public partial class LogContext : DbContext
    {
        public LogContext()
        {
        }

        public LogContext(DbContextOptions<LogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogMessage> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogMessage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
