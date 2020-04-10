using System.Reflection;

using Microsoft.EntityFrameworkCore;

using DataBaseService.Database.Models;

namespace DataBaseService.Database
{
    public class TPlatformLogsDbContext : DbContext
    {
        public DbSet<DbLogs> Logs { get; set; }

        public TPlatformLogsDbContext(DbContextOptions<TPlatformLogsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
