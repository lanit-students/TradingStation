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
        private readonly IConfiguration configuration;

        public MigrationEngine(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Migrate()
        {
            var connectionString = configuration.GetConnectionString("MigrationString");
            var location = configuration.GetSection("Locations")["MigrationScripts"];
            var fileNames = Directory.GetFiles(location, "*.sql");
            var executedScripts = new Dictionary<string, string>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (string fileName in fileNames)
                {
                    bool exists = false;
                    try
                    {
                        SqlCommand command = new SqlCommand("USE [TradingStation]; SELECT (FileName) FROM [dbo].[ExecutedScripts] WHERE (FileName = @fileName);", conn);
                        command.Parameters.AddWithValue("@fileName", fileName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            exists = reader.Read();
                        }
                    }
                    catch
                    {
                        
                    }

                    if (!exists)
                    {
                        var scriptString = File.ReadAllText(fileName);
                        try
                        {
                            using (var command = new SqlCommand(scriptString, conn))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            throw;
                        }

                        try
                        {
                            using (var command = new SqlCommand($"USE [TradingStation]; INSERT INTO [dbo].[ExecutedScripts] (FileName, Code) VALUES('{fileName}', 'scriptString');", conn))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            executedScripts.Add(fileName, scriptString);
                        }
                    }
                }
                foreach (var script in executedScripts)
                {
                    using (var command = new SqlCommand($"USE [TradingStation]; INSERT INTO [dbo].[ExecutedScripts] (FileName, Code) VALUES('{script.Key}', 'scriptString');", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
