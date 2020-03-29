using DataBaseService.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataBaseService
{
    public class DataBaseContext: DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<DbUserCredential> UsersCredentials { get; set; }
        

        public DataBaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringTradingStation = configuration.GetConnectionString("TradingStationString");

            optionsBuilder.UseSqlServer(connectionStringTradingStation);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                        
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
