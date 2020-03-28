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
            //modelBuilder.Entity<DbUser>().ToTable("Users");
            //modelBuilder.Entity<DbUser>().Property(u => u.Id).HasColumnName("Id").IsRequired();
            //modelBuilder.Entity<DbUser>().Property(u => u.Email).HasColumnName("Email").IsRequired();
            //modelBuilder.Entity<DbUser>().Property(u => u.Password).HasColumnName("Password").IsRequired();  
            //modelBuilder.Entity<DbUser>().HasKey(u => u.Id);            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
