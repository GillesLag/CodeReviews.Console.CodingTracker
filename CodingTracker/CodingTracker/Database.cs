using Microsoft.Data.Sqlite;
using System.Configuration;

namespace CodingTracker;

internal static class Database
{
    private static readonly string _connectionString = ConfigurationManager.AppSettings.Get("Connectionstring") ?? 
        throw new Exception("Key value pair doesn't exist in the config-file!");

    private static readonly string _databasePath = ConfigurationManager.AppSettings.Get("DatabasePath") ?? 
        throw new Exception("Key value pair doesn't exist in the config-file!");
    internal static void CreateDatabase()
    {
        if (!File.Exists(_databasePath))
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"CREATE TABLE CodingSession
                    (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                        StartTime TEXT NOT NULL,
                        EndTime TEXT NOT NULL,
                        Duration INTEGER NOT NULL
                    )";

                command.ExecuteNonQuery();
            }
        }
    }
}
