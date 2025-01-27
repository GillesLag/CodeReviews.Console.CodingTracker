using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;

namespace CodingTracker.Controllers;

internal class CodingSessionController
{
    protected readonly string _connectionString = ConfigurationManager.AppSettings.Get("Connectionstring") ?? throw new Exception("Key value pair doesn't exist in the config-file!");

    public List<CodingSession> GetAll()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT * FROM CodingSession";

            connection.Open();

            return connection.Query<CodingSession>(query).ToList();
        }
    }

    public CodingSession GetById(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string query = "SELECT * FROM CodingSession WHERE Id = @Id";

            connection.Open();

            return connection.Query<CodingSession>(query, new { Id = id }).First();
        }
    }

    /// <summary>
    /// Returns the number of rows affected
    /// </summary>
    /// <param name="codingSession"></param>
    /// <returns></returns>
    public int Add(CodingSession codingSession)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = "INSERT INTO CodingSession (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

            connection.Open();

            return connection.Execute(sql, codingSession);
        }
    }

    /// <summary>
    /// Returns the number of rows affected
    /// </summary>
    /// <param name="codingSession"></param>
    /// <returns></returns>
    public int Update(CodingSession codingSession)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = "UPDATE CodingSession SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE id = @Id";

            connection.Open();

            return connection.Execute(sql, codingSession);
        }
    }

    /// <summary>
    /// Returns the number of rows affected
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int Delete(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = "DELETE FROM CodingSession WHERE id = @Id";

            connection.Open();

            return connection.Execute(sql, new { Id = id});
        }
    }

    internal bool Exists(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = "SELECT * FROM CodingSession WHERE id = @Id";

            connection.Open();

            var reader = connection.ExecuteScalar(sql, new { Id = id });

            return reader != null;
        }

    }
}
