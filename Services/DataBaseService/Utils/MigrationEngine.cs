using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

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

            try
            {
                using (var conn = new SqlConnection(connectionStringTradingStation))
                {
                    conn.Open();
                    SqlTransaction executingTransaction = conn.BeginTransaction();
                    try
                    {
                        foreach (var fileName in scriptsToExecute)
                        {
                            lastScriptToExecute = fileName;
                            var scriptCode = File.ReadAllText(fileName);
                            using (var command = new SqlCommand(scriptCode, conn, executingTransaction))
                                command.ExecuteNonQuery();
                            scriptsToWriteDown.Add(fileName, new ExecutedScript(DateTime.Now, scriptCode));
                        }
                        executingTransaction.Commit();
                        WriteDownExecutedScripts(connectionStringTradingStation, scriptsToWriteDown);
                        Console.WriteLine("\tExecuted scripts written down.");
                    }
                    catch (SqlException e)
                    {
                        // TODO replace with logs
                        Console.WriteLine(e.Message +
                            $"\n\tError in the [{lastScriptToExecute}] script, or the connection is broken.");
                        try
                        {
                            executingTransaction.Rollback();
                            // TODO replace with logs
                            Console.WriteLine("\tExecuting rollback is successful.");
                        }
                        catch
                        {
                            // TODO replace with logs
                            Console.WriteLine("\tCouldn't rollback executing.");
                            WriteDownExecutedScripts(connectionStringTradingStation, scriptsToWriteDown);
                            Console.WriteLine("\tExecuted scripts were written down.");
                        }
                        throw;
                    }
                    finally
                    {
                        executingTransaction.Dispose();
                    }
                }
            }
            catch (SqlException e) when (e.Number == -2)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't open the connection (timeout).");
                throw;
            }
            catch (Exception e) when (!(e is SqlException))
            {
                // TODO replace with logs
                Console.WriteLine(e.Message);
                throw;
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

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction writingTransaction = conn.BeginTransaction();
                    try
                    {
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
                    }
                    catch (SqlException e)
                    {
                        // TODO replace with logs
                        Console.WriteLine(e.Message +
                            $"\n\tWarning! The [{lastScriptToWrite}] script cannot be marked as executed.");
                        try
                        {
                            writingTransaction.Rollback();
                            // TODO replace with logs
                            Console.WriteLine("\tWriting rollback is successful.");
                        }
                        catch
                        {
                            // TODO replace with logs
                            Console.WriteLine("\tCouldn't rollback writing.");
                        }
                        throw e;
                    }
                    finally
                    {
                        writingTransaction.Dispose();
                    }
                }
            }
            catch (SqlException e) when (e.Number == -2)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't write down executed scripts: the connection timeout.");
                throw e;
            }
            catch (Exception e) when (!(e is SqlException))
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't write down executed scripts.");
                throw e;
            }
        }

        private List<string> FilterScriptsToExecute(string connectionString, string[] allScripts)
        {
            var executedScripts = new List<string>();
            var scriptsToExecute = new List<string>();
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
                foreach (var scriptName in allScripts)
                    if (!(executedScripts.Contains(scriptName)))
                        scriptsToExecute.Add(scriptName);
                return scriptsToExecute;
            }
            catch (SqlException e) when (e.Number == -2)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + 
                    "\n\tCouldn't receive the list of executed scripts: the connection timeout.");
                throw e;
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't receive the list of executed scripts.");
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
                    // TODO replace with logs
                    Console.WriteLine("\tTradingStation DB was created successfully or already existed.");
                    return new ExecutedScript(DateTime.Now, createDbScript);
                }
            }
            catch (SqlException e) when (e.Number == -2)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't create the TradingStation DB: the connection timeout.");
                throw e;
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't create the TradingStation DB.");
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
                    // TODO replace with logs
                    Console.WriteLine("\tThe Scripts table was created successfully or already existed.");
                    return new ExecutedScript(DateTime.Now, createTableScript.ToString());
                }
            }
            catch (SqlException e) when (e.Number == -2)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't create Scripts table: the connection timeout.");
                throw e;
            }
            catch (Exception e)
            {
                // TODO replace with logs
                Console.WriteLine(e.Message + "\n\tCouldn't create the Scripts table.");
                throw e;
            }
        }
    }
}
