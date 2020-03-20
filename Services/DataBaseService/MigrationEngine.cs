using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
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
            var connectionString = configuration.GetConnectionString("TradingStationString");
            var scriptsToWriteDown = new Dictionary<string, ExecutedScript>
            {
                { "CreateDatabase", CreateDatabase() },
                { "CreateScriptsTable", CreateScriptsTable(connectionString) }
            };
            List<string> scriptsToExecute = GetScriptsToExecute(connectionString);

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction executingTransaction = conn.BeginTransaction();
                foreach (var fileName in scriptsToExecute)
                {
                    var scriptCode = File.ReadAllText(fileName);
                    try
                    {
                        using (var command = new SqlCommand(scriptCode, conn, executingTransaction))
                            command.ExecuteNonQuery();
                        scriptsToWriteDown.Add(fileName, new ExecutedScript(DateTime.Now, scriptCode));
                    }
                    catch (SqlException e)
                    {
                        // TODO replace with logs
                        Console.WriteLine(e.Message + $"\n\tError in the {fileName} script," +
                            "\nor the connection is broken.");
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
                        }
                        throw;
                    }
                }
                executingTransaction.Commit();
                executingTransaction.Dispose();
            }

            WriteDownExecutedScripts(connectionString, scriptsToWriteDown);
        }

        private void WriteDownExecutedScripts(string connectionString, 
            Dictionary<string, ExecutedScript> scriptsToWriteDown)
        {
            var insertRow = new StringBuilder();
            insertRow.Append("INSERT INTO [dbo].[ExecutedScripts] ");
            insertRow.Append("(FileName, ExecutionTime, Code) ");
            insertRow.Append("VALUES (@fileName, @executionTime, @code);");

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction writingTransaction = conn.BeginTransaction();
                foreach (var script in scriptsToWriteDown)
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(insertRow.ToString(), conn, writingTransaction))
                        {
                            command.Parameters.AddWithValue("@fileName", script.Key);
                            command.Parameters.AddWithValue("@executionTime", script.Value.ExecutionTime);
                            command.Parameters.AddWithValue("@code", script.Value.ExecutedCode);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException e)
                    {
                        // TODO replace with logs
                        Console.WriteLine(e.Message + 
                            $"\n\tWarning!\n{script.Key} script cannot be marked as executed.");
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
                        throw;
                    }
                }
                writingTransaction.Commit();
                writingTransaction.Dispose();
            }
        }

        private List<string> GetScriptsToExecute(string connectionString)
        {
            var scriptsLocation = configuration.GetSection("Locations")["MigrationScripts"];
            var allScripts = Directory.GetFiles(scriptsLocation, "*.sql");
            var scriptsToExecute = new List<string>();
            var selectFileName = "SELECT (FileName) FROM [dbo].[ExecutedScripts] WHERE (FileName = @fileName);";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var fileName in allScripts)
                {
                    bool alreadyExecuted;
                    try
                    {
                        using (SqlCommand command = new SqlCommand(selectFileName, conn))
                        {
                            command.Parameters.AddWithValue("@fileName", fileName);
                            alreadyExecuted = (command.ExecuteScalar() is null) ? false : true;
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message + $"\n\tCouldn't check if {fileName} was already executed.");
                        alreadyExecuted = false;
                    }

                    if (!alreadyExecuted)
                        scriptsToExecute.Add(fileName);
                }
            }
            return scriptsToExecute;
        }

        private ExecutedScript CreateDatabase()
        {
            var connectionString = configuration.GetConnectionString("MigrationString");
            var createDbScript = "IF DB_ID('TradingStation') IS NULL CREATE DATABASE[TradingStation];";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(createDbScript, conn))
                        command.ExecuteNonQuery();
                    // TODO replace with logs
                    Console.WriteLine("\tTradingStation DB was created successfully or already existed.");
                    return new ExecutedScript(DateTime.Now, createDbScript);
                }
                catch (SqlException e)
                {
                    // TODO replace with logs
                    Console.WriteLine(e.Message + "\n\tCouldn't create the TradingStation DB.");
                    throw;
                }
            }
        }

        private ExecutedScript CreateScriptsTable(string connectionString)
        {
            var createTableScript = new StringBuilder();
            createTableScript.Append("IF OBJECT_ID('dbo.ExecutedScripts', 'U') IS NULL ");
            createTableScript.Append("CREATE TABLE [dbo].[ExecutedScripts] "); 
            createTableScript.Append("(Id INT IDENTITY NOT NULL, ");
            createTableScript.Append("FileName NVARCHAR(100) NOT NULL, ");
            createTableScript.Append("ExecutionTime DATETIME NOT NULL, ");
            createTableScript.Append("Code NVARCHAR(MAX) NOT NULL, ");
            createTableScript.Append("CONSTRAINT PKscripts PRIMARY KEY CLUSTERED(Id));");

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(createTableScript.ToString(), conn))
                        command.ExecuteNonQuery();
                    // TODO replace with logs
                    Console.WriteLine("\tThe Scripts table was created successfully or already existed.");
                    return new ExecutedScript(DateTime.Now, createTableScript.ToString());
                }
                catch (SqlException e)
                {
                    // TODO replace with logs
                    Console.WriteLine(e.Message + "\n\tCouldn't create the Scripts table.");
                    throw;
                }
            }
        }

        internal struct ExecutedScript
        {
            internal DateTime ExecutionTime { get; }
            internal string ExecutedCode { get; }

            internal ExecutedScript(DateTime executionTime, string executedCode)
            {
                ExecutionTime = executionTime;
                ExecutedCode = executedCode;
            }
        }
    }
}
