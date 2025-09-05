using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace discorddb
{

    public static class DatabaseHelper
    {
        private static string connectionString = @"Data Source=..\..\Files\discorddb.db; Version=3";
        public static void InitialiseDatabase()
        {
            SQLiteConnection.CreateFile(@"..\..\Files\discorddb.db");

            using(var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (var command = new SQLiteCommand(connection))
                {
                    //new code
                    command.CommandText = @"
                 CREATE TABLE IF NOT EXISTS Message(
                        Id INTEGER PRIMARY KEY,
                        Content TEXT,
                        Date DATETIME,
                        ChannelId TEXT,
                        ChannelType TEXT,
                        Attachments TEXT
                     );";

                    command.ExecuteNonQuery();
                }
            }
            
        }
        public static string getConnectionString()
        {
            return connectionString;
        }
        public static void UpdateDatabase(SQLiteCommand command)
        {
            
            command.ExecuteNonQuery();  
        }
        public static SQLiteCommand ConnectToDatabase(string pConnectionString) 
        {
            var connection = new SQLiteConnection(pConnectionString);
            connection.Open();
            return new SQLiteCommand(connection);

        }
    }
}
