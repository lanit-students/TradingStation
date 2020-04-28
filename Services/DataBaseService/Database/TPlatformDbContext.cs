using System.Reflection;

using Microsoft.EntityFrameworkCore;

using DataBaseService.Database.Models;

namespace DataBaseService.Database
{
    public class TPlatformDbContext : DbContext
    {
        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbUserCredential> UsersCredentials { get; set; }
        public DbSet<DbUsersAvatars> UsersAvatars { get; set; }
        public DbSet<DbTransaction> Transactions { get; set; }
        public DbSet<DbPortfolioInstruments> PortfolioInstruments { get; set; }
        public DbSet<DbBrokerUser> BrokerUsers { get; set; }
        public TPlatformDbContext(DbContextOptions<TPlatformDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
