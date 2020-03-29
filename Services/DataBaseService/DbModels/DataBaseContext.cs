using DataBaseService.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
            try
            {
                optionsBuilder.UseSqlServer(connectionStringTradingStation);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "Something went wrong");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                        
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
