using System.Reflection;

using Microsoft.EntityFrameworkCore;

using DataBaseService.Database.Models;

namespace DataBaseService.Database
{
    public class TPlatformDbContext : DbContext
    {
        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbUserCredential> UsersCredentials { get; set; }
        public DbSet<DbUserAvatar> UserAvatars { get; set; }

        public TPlatformDbContext(DbContextOptions<TPlatformDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
