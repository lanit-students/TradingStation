using System.Reflection;

using Microsoft.EntityFrameworkCore;

using DataBaseService.Database.Models;

namespace DataBaseService.Database
{
    public class TPlatformLogDbContext : DbContext
    {
        public DbSet<DbLog> Logs { get; set; }

        public TPlatformLogDbContext(DbContextOptions<TPlatformLogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
