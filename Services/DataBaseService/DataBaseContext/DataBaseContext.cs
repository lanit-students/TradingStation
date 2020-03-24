using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseService.DbModels;
using Microsoft.EntityFrameworkCore;


namespace DataBaseService
{
    public class DataBaseContext: DbContext
    {
        public DbSet<DbUser> Users { get; set; }

        public DataBaseContext()
        {
            Database.EnsureCreated();            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
            Console.WriteLine("Connection string was configured");
        }

    }
}
