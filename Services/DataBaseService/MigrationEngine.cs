using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DataBaseService
{
    public class MigrationEngine
    {
        public static string Write()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultString"].ConnectionString;
            //var connectionString = "Server=.\\SQLEXPRESS;Database=TradingStation;Trusted_Connection=True;MultipleActiveResultSets=true";
            var connectionString = GetConnectionString();
            var scriptString = File.ReadAllText("MigrationScripts/03.2020/01_TradingStationDBCreate.sql");
            
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new SqlCommand("InsertNewScriptRow", conn) 
                    { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@Code", scriptString);
                    command.ExecuteNonQuery();
                }
            }

            return connectionString;
        }

        static string GetConnectionString()

        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();
            var connectionString = configuration.GetConnectionString("MigrationString");
            //var connectionString = ConfigurationManager.ConnectionStrings["MigrationString"].ConnectionString;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(connectionString);
            Console.WriteLine();
            Console.WriteLine();
            return connectionString;

        }
    }
}
