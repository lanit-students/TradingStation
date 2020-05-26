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
        public DbSet<DbPortfolio> Portfolios { get; set; }
        public DbSet<DbUserBalance> UserBalances { get; set; }
        public DbSet<DbBotRule> BotRules { get; set; }
        public DbSet<DbLinkBotsWithRules> LinkBotsWithRules { get; set; }
        public DbSet<DbBot> Bots { get; set; }

        public TPlatformDbContext(DbContextOptions<TPlatformDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
