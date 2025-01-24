using CodingTracker;
using System.Configuration;

string connectionString = ConfigurationManager.AppSettings.Get("Connectionstring") ?? throw new Exception("Key value pair doesn't exist in the config-file!");

Database.CreateDatabase(connectionString);

