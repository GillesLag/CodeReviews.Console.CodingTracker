using System.Configuration;

string connectionstring = ConfigurationManager.AppSettings.Get("Connectionstring") ?? throw new Exception("Key value pair doesn't exist in the config-file!");