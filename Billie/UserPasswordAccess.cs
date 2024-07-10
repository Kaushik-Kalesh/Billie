using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Windows.Storage;

namespace Billie
{
    public static class UserPasswordAccess
    {
        private static string dbFileName = "user_info.db";
        private static string dbPath;
        private static string tableName = "Passwords";
        private interface FieldNames
        {
            const string PrimaryKey = "Primary_Key";
            const string UserType = "User_Type";
            const string Password = "Password";
        };

        public async static void InitializeDatabase()
        {            
            await ApplicationData.Current.LocalFolder
                .CreateFileAsync(dbFileName, CreationCollisionOption.OpenIfExists);
         
            dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbFileName);
            
            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();
                
                string tableCommand = "CREATE TABLE IF NOT EXISTS Passwords (Primary_Key INTEGER PRIMARY KEY, User_Type NVARCHAR(10), " +
                                      "Password NVARCHAR(8) NULL);";

                using (var createTable = new SqliteCommand(tableCommand, db))
                {
                    createTable.ExecuteNonQuery();
                }

                // Check if there are any entries in the table
                string countCommand = $"SELECT COUNT(*) FROM {tableName};";
                int count = 0;

                using (var selectCommand = new SqliteCommand(countCommand, db))
                using (var query = selectCommand.ExecuteReader())
                {
                    while (query.Read())
                    {
                        count = query.GetInt32(0);
                    }
                }

                // Insert default entries if the table is empty
                if (count == 0)
                {
                    string insertCommand = $"INSERT INTO {tableName} ({FieldNames.UserType}) VALUES (@UserType);";
                    using (var insertCommandCashier = new SqliteCommand(insertCommand, db))
                    {
                        insertCommandCashier.Parameters.AddWithValue("@UserType", "Cashier");
                        insertCommandCashier.ExecuteNonQuery();
                    }

                    using (var insertCommandOwner = new SqliteCommand(insertCommand, db))
                    {
                        insertCommandOwner.Parameters.AddWithValue("@UserType", "Owner");
                        insertCommandOwner.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void UpdatePassword(UserType userType, string password)
        {
            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();

                var updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = $"UPDATE {tableName} SET {FieldNames.Password}=@Password WHERE {FieldNames.UserType}=@UserType;";
                updateCommand.Parameters.AddWithValue("@UserType", userType == UserType.Cashier ? "Cashier" : "Owner");
                updateCommand.Parameters.AddWithValue("@Password", password);

                updateCommand.ExecuteReader();
            }
        }

        public static string GetPassword(UserType userType)
        {           
            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();
                try
                {
                    var selectCommand = new SqliteCommand();
                    selectCommand.Connection = db;

                    selectCommand.CommandText = $"SELECT {FieldNames.Password} FROM {tableName} WHERE {FieldNames.UserType}=@UserType";
                    selectCommand.Parameters.AddWithValue("@UserType", userType == UserType.Cashier ? "Cashier" : "Owner");                 

                    SqliteDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        return query.GetString(0);
                    }
                }
                catch { }
            }

            return null;
        }
    }
}