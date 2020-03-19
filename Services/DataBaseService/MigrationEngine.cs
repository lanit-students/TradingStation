using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        /// <summary>
        /// Migration method. 
        /// Runs through all the .sql in the directory (configure in the appsettings.json).
        /// Checks (by its name) if a script was already executed, executes it, 
        /// and marks as executed (by adding a row to the table).
        /// </summary>
        public void Migrate()
        {
            var connectionString = configuration.GetConnectionString("MigrationString");
            var scriptsLocation = configuration.GetSection("Locations")["MigrationScripts"];
            var scriptsToExecute = Directory.GetFiles(scriptsLocation, "*.sql");
            var scriptsToWriteDown = new Dictionary<string, string>();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var fileName in scriptsToExecute)
                {
                    bool alreadyExecuted = false;
                    try
                    {
                        using (SqlCommand command = new SqlCommand("USE [TradingStation]; SELECT (FileName) " +
                            "FROM [dbo].[ExecutedScripts] WHERE (FileName = @fileName);", conn))
                        {
                            command.Parameters.AddWithValue("@fileName", fileName);
                            alreadyExecuted = (command.ExecuteScalar() is null) ? false : true;
                        }
                    }
                    catch (SqlException e)
                    {
                        // TODO replace with logs
                        Console.WriteLine(e.Message + "\n\tThe DB or the table is to be created now.");
                    }

                    if (!alreadyExecuted)
                    {
                        var scriptString = File.ReadAllText(fileName);
                        try
                        {
                            using (var command = new SqlCommand(scriptString, conn))
                                command.ExecuteNonQuery();
                            scriptsToWriteDown.Add(fileName, scriptString);
                        }
                        catch (SqlException e)
                        {
                            // TODO replace with logs
                            Console.WriteLine(e.Message + $"\n\tError in the {fileName} script," +
                                "\nor the connection is broken.");
                            throw;
                        }
                    }
                }

                foreach (var script in scriptsToWriteDown)
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("USE [TradingStation]; INSERT INTO " +
                            "[dbo].[ExecutedScripts] (FileName, Code) VALUES (@fileName, @code);", conn))
                        {
                            command.Parameters.AddWithValue("@fileName", script.Key);
                            command.Parameters.AddWithValue("@code", script.Value);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException e)
                    {
                        // TODO replace with logs
                        Console.WriteLine(e.Message + $"\n\tWarning!\n{script.Key} " +
                            "script cannot be marked as executed.");
                        throw;
                    }
                }
            }
        }
    }
}
