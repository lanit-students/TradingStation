using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DataBaseService.Utils
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
        /// Creates "TradingStation" DB with "ExecutedScripts" table, then:
        /// Runs through all the .sql in the directory (configure in the appsettings.json).
        /// Checks (by its name) if a script was already executed, executes it, 
        /// and marks as executed (by adding a row to the table).
        /// </summary>
        public void Migrate()
        {
            var connectionStringInitial = configuration.GetConnectionString("InitialString");
            var connectionStringTradingStation = configuration.GetConnectionString("TradingStationString");
            var scriptsLocation = configuration.GetSection("Locations")["MigrationScripts"];
            var allScripts = Directory.GetFiles(scriptsLocation, "*.sql");
            var scriptsToWriteDown = new Dictionary<string, ExecutedScript>
            {
                { "CreateDatabase", CreateDatabase(connectionStringInitial) },
                { "CreateScriptsTable", CreateScriptsTable(connectionStringTradingStation) }
            };
            List<string> scriptsToExecute = FilterScriptsToExecute(connectionStringTradingStation, allScripts);
            var lastScriptToExecute = "no script selected";
            SqlTransaction executingTransaction = null;

            try
            {
                using (var conn = new SqlConnection(connectionStringTradingStation))
                {
                    conn.Open();
                    executingTransaction = conn.BeginTransaction();
                    foreach (var fileName in scriptsToExecute)
                    {
                        lastScriptToExecute = fileName;
                        var scriptCode = File.ReadAllText(fileName);
                        using (var command = new SqlCommand(scriptCode, conn, executingTransaction))
                            command.ExecuteNonQuery();
                        scriptsToWriteDown.Add(fileName, new ExecutedScript(DateTime.Now, scriptCode));
                    }
                    executingTransaction.Commit();
                }
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + $"\n\tExecution error on the [{lastScriptToExecute}] script.");
                // TODO replace with logs
                Console.WriteLine("\tExecution transaction rollbacked.");
            }
            finally
            {
                executingTransaction?.Dispose();
                WriteDownExecutedScripts(connectionStringTradingStation, scriptsToWriteDown);
            }
        }

        private void WriteDownExecutedScripts(string connectionString, 
            Dictionary<string, ExecutedScript> scriptsToWriteDown)
        {
            var insertRow = new StringBuilder();
            insertRow.AppendLine("INSERT INTO [dbo].[ExecutedScripts] ");
            insertRow.AppendLine("(FileName, ExecutionTime, Code) ");
            insertRow.AppendLine("VALUES (@fileName, @executionTime, @code);");
            var lastScriptToWrite = "no script written";
            SqlTransaction writingTransaction = null;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    writingTransaction = conn.BeginTransaction();
                    foreach (var script in scriptsToWriteDown)
                        using (var command = new SqlCommand(insertRow.ToString(), conn, writingTransaction))
                        {
                            lastScriptToWrite = script.Key;
                            command.Parameters.AddWithValue("@fileName", script.Key);
                            command.Parameters.AddWithValue("@executionTime", script.Value.ExecutionTime);
                            command.Parameters.AddWithValue("@code", script.Value.ExecutedCode);
                            command.ExecuteNonQuery();
                        }
                    writingTransaction.Commit();
                    // TODO replace with logs
                    Console.WriteLine("\tExecuted scripts written down.");
                }
            }
            catch (SqlException e)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + 
                    $"\n\tCouldn't write down executed scripts. Error on the [{lastScriptToWrite}] script.");
                // TODO replace with logs
                Console.WriteLine("\tWriting transaction rollbacked.");
            }
            finally
            {
                writingTransaction?.Dispose();
            }
        }

        private List<string> FilterScriptsToExecute(string connectionString, string[] allScripts)
        {
            var executedScripts = new List<string>();
            var selectFileName = "SELECT (FileName) FROM [dbo].[ExecutedScripts];";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(selectFileName, conn))
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                            executedScripts.Add(reader.GetString(0));
                }
                return allScripts.ToList().Except(executedScripts).ToList();
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine("\n\tCouldn't receive the list of executed scripts.");
                throw e;
            }
        }

        private ExecutedScript CreateDatabase(string connectionString)
        {
            var createDbScript = "IF DB_ID('TradingStation') IS NULL CREATE DATABASE [TradingStation];";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(createDbScript, conn))
                        command.ExecuteNonQuery();
                    return new ExecutedScript(DateTime.Now, createDbScript);
                }
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine("\n\tCouldn't create the TradingStation DB.");
                throw e;
            }
        }

        private ExecutedScript CreateScriptsTable(string connectionString)
        {
            var createTableScript = new StringBuilder();
            createTableScript.AppendLine("IF OBJECT_ID('dbo.ExecutedScripts', 'U') IS NULL ");
            createTableScript.AppendLine("CREATE TABLE [dbo].[ExecutedScripts] "); 
            createTableScript.AppendLine("(Id INT IDENTITY NOT NULL, ");
            createTableScript.AppendLine("FileName NVARCHAR(100) NOT NULL, ");
            createTableScript.AppendLine("ExecutionTime DATETIME NOT NULL, ");
            createTableScript.AppendLine("Code NVARCHAR(MAX) NOT NULL, ");
            createTableScript.AppendLine("CONSTRAINT PKscripts PRIMARY KEY CLUSTERED(Id));");

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(createTableScript.ToString(), conn))
                        command.ExecuteNonQuery();
                    return new ExecutedScript(DateTime.Now, createTableScript.ToString());
                }
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine("\n\tCouldn't create the Scripts table.");
                throw e;
            }
        }
    }
}
