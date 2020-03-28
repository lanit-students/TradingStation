using DataBaseService.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataBaseService
{
    public class DataBaseContext: DbContext
    {
        public DbSet<DbUser> Users { get; set; }

        private readonly IConfiguration configuration;

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
