using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace Truphone.Database.Bootstrap
{
    class BootstrapInMemoryDB
    {
        public void Setup()
        {
            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.
            var masterConnection = new SqliteConnection(DBConfig.ConnectionString);
            masterConnection.Open();

            // Check if the table already exists.
            var table = masterConnection.Query<string>
                (@"SELECT name FROM sqlite_master WHERE type='table' AND name = 'Devices';");

            var tableName = table.FirstOrDefault();
            if (tableName != default) return;

            // Create the table.
            masterConnection.Execute(
                @"CREATE TABLE Devices (
                Id INTEGER PRIMARY KEY NOT NULL,
                Name VARCHAR(250) NOT NULL,
                Brand VARCHAR(250) NULL,
                DateCreated DATETIME NULL,
                DateUpdated DATETIME NULL);");
        }
    }
}
