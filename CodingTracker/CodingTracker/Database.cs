using Microsoft.Data.Sqlite;

namespace CodingTracker;

internal static class Database
{
    internal static void CreateDatabase(string connectionString)
    {
        if (!File.Exists("CodingTracker.db"))
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"CREATE TABLE CodingSession
                    (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                        StartTime TEXT NOT NULL,
                        EndTime TEXT NOT NULL,
                        Duration TEXT NOT NULL
                    )";

                command.ExecuteNonQuery();
            }
        }
    }
}
