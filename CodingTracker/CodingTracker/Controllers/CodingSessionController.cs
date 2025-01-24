using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controllers;

internal class CodingSessionController : BaseController
{
    public CodingSessionController(string connectionString) : base(connectionString)
    {
    }

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

    public int Update(CodingSession codingSession)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = "UPDATE CodingSession SET @StartTime, @EndTime, @Duration WHERE id = @Id";

            connection.Open();

            return connection.Execute(sql, codingSession);
        }
    }

    public int Delete(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = "DELETE FROM CodingSession WHERE id = @Id";

            connection.Open();

            return connection.Execute(sql, new { Id = id});
        }
    }
}
