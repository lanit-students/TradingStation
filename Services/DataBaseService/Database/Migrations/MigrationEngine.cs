using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DataBaseService.Database.Migrations;
using Kernel.CustomExceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataBaseService.Utils
{
    public class MigrationEngine
    {
        private const string ExecutedScriptsTableName = "__ExecutedScripts";
        private const string LogsTableName = "Logs";
        private const string ScriptsFolderName = "DataBaseService.Database.Migrations.Scripts";

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

            var allScripts = GetAllScripts();
            if (allScripts?.Count == 0)
                return;

            CreateLogsDatabase(connectionStringInitial);

            CreateLogsTable(connectionStringInitial);

            CreateDatabase(connectionStringInitial);

            CreateExecutedScriptsTable(connectionStringTradingStation);

            List<ScriptFile> executedScripts = GetAlreadyExecutedScriptsFromDatabase(connectionStringTradingStation);

            var scriptsToExecute = allScripts.Where(s => executedScripts.All(es => es.Name != s.Name)).ToList();

            SqlTransaction dbUpdateTransaction = null;

            var scriptsForSaveInDatabase = new List<ScriptFile>();

            try
            {
                using var conn = new SqlConnection(connectionStringTradingStation);
                conn.Open();

                dbUpdateTransaction = conn.BeginTransaction("DB_UPDATE");

                foreach (ScriptFile script in scriptsToExecute)
                {
                    List<string> queries = SplitByGoWord(script.Content);
                    foreach (string query in queries)
                    {
                        using var command = new SqlCommand(query, conn, dbUpdateTransaction);
                        command.ExecuteNonQuery();
                    }

                    scriptsForSaveInDatabase.Add(script);
                }

                dbUpdateTransaction.Commit();
            }
            catch (Exception exc)
            {
                throw new InternalServerException("Can't execute migration scripts.", exc);
            }
            finally
            {
                dbUpdateTransaction?.Dispose();

                WriteDownExecutedScripts(connectionStringTradingStation, scriptsForSaveInDatabase);
            }
        }

        private void WriteDownExecutedScripts(
            string connectionString,
            List<ScriptFile> scriptsForSaveInDatabase)
        {
            var insertQuery = new StringBuilder();
            insertQuery.AppendLine($"INSERT INTO [dbo].[{ExecutedScriptsTableName}] ");
            insertQuery.AppendLine("(FileName, ExecutionTime, Code) ");
            insertQuery.AppendLine("VALUES (@fileName, @executionTime, @code);");

            SqlTransaction writingTransaction = null;

            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                writingTransaction = conn.BeginTransaction();

                foreach (var script in scriptsForSaveInDatabase)
                {
                    using var command = new SqlCommand(insertQuery.ToString(), conn, writingTransaction);
                    command.Parameters.AddWithValue("@fileName", script.Name);
                    command.Parameters.AddWithValue("@executionTime", DateTime.Now);
                    command.Parameters.AddWithValue("@code", script.Content);
                    command.ExecuteNonQuery();
                }

                writingTransaction.Commit();

                // TODO add logs for executed script
            }
            catch (Exception exc)
            {
                throw new InternalServerException("Can't execute migration scripts.", exc);
            }
            finally
            {
                writingTransaction?.Dispose();
            }
        }

        private List<ScriptFile> GetAlreadyExecutedScriptsFromDatabase(string connectionString)
        {
            var result = new List<ScriptFile>();

            var query = $"SELECT FileName FROM [dbo].[{ExecutedScriptsTableName}];";

            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var command = new SqlCommand(query, conn);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ScriptFile
                    {
                        Name = reader["FileName"].ToString()
                    });
                }
            }
            catch (Exception exc)
            {
                throw new InternalServerException("Can't execute migration scripts.", exc);
            }

            return result;
        }

        private void CreateLogsDatabase(string connectionString)
        {
            var createLogDbScript = "IF DB_ID('TradingStationLogs') IS NULL CREATE DATABASE [TradingStationLogs];";

            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var command = new SqlCommand(createLogDbScript, conn);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw new InternalServerException();
            }
        }

        private void CreateDatabase(string connectionString)
        {
            var createDbScript = "IF DB_ID('TradingStation') IS NULL CREATE DATABASE [TradingStation];";

            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var commandFirst = new SqlCommand(createDbScript, conn);
                commandFirst.ExecuteNonQuery();
            }
            catch
            {
                throw new InternalServerException();
            }
        }

        private void CreateLogsTable(string connectionString)
        {
            var createLogsTableScript = new StringBuilder();
            createLogsTableScript.AppendLine("USE [TradingStationLogs]; ");
            createLogsTableScript.AppendLine($"IF OBJECT_ID('dbo.[{LogsTableName}]', 'U') IS NULL ");
            createLogsTableScript.AppendLine($"CREATE TABLE [dbo].[{LogsTableName}] ");
            createLogsTableScript.AppendLine("([Id] [uniqueidentifier] NOT NULL, ");
            createLogsTableScript.AppendLine("[Level] [int] NOT NULL, ");
            createLogsTableScript.AppendLine("[Time] [datetime] NOT NULL, ");
            createLogsTableScript.AppendLine("[Message] [nvarchar](max) NOT NULL, ");
            createLogsTableScript.AppendLine("[ServiceName] [nvarchar](50) NOT NULL, ");
            createLogsTableScript.AppendLine("[ParentId] [uniqueidentifier] NULL, ");
            createLogsTableScript.AppendLine("CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED(Id));");

            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var command = new SqlCommand(createLogsTableScript.ToString(), conn);
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new InternalServerException("Can't execute migration scripts.", exc);
            }
        }

        private void CreateExecutedScriptsTable(string connectionString)
        {
            var createTableScript = new StringBuilder();
            createTableScript.AppendLine($"IF OBJECT_ID('dbo.[{ExecutedScriptsTableName}]', 'U') IS NULL ");
            createTableScript.AppendLine($"CREATE TABLE [dbo].[{ExecutedScriptsTableName}] ");
            createTableScript.AppendLine("(Id INT IDENTITY NOT NULL, ");
            createTableScript.AppendLine("FileName NVARCHAR(MAX) NOT NULL, ");
            createTableScript.AppendLine("ExecutionTime DATETIME NOT NULL, ");
            createTableScript.AppendLine("Code NVARCHAR(MAX) NOT NULL, ");
            createTableScript.AppendLine("CONSTRAINT PKscripts PRIMARY KEY CLUSTERED(Id));");

            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var command = new SqlCommand(createTableScript.ToString(), conn);
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new InternalServerException("Can't execute migration scripts.", exc);
            }
        }

        private static List<ScriptFile> GetAllScripts()
        {
            List<ScriptFile> result = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(r => r.EndsWith(".sql"))
                .OrderBy(r => r)
                .Select(r => new ScriptFile
                {
                    Name = r.Replace(ScriptsFolderName + ".", ""),
                    Content = GetScriptContent(r),
                })
                .ToList();

            return result;
        }

        private static string GetScriptContent(string scriptFileName)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(scriptFileName);
            if (stream == null)
                throw new InternalServerException();

            using StreamReader streamReader = new StreamReader(stream);

            return streamReader.ReadToEnd();
        }

        private List<string> SplitByGoWord(string scriptBody)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                    scriptBody,
                    @"^\s*GO\s*\d*\s*($|\-\-.*$)",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'))
                .ToList();
        }
    }
}
