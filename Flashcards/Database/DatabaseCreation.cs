using Dapper;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Flashcards.Database
{
    internal class DatabaseCreation
    {
        private string connectionStr = ConfigurationManager.AppSettings.Get("ConnectionString");

        internal void CreateDatabase()
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                var createStackTable = @"IF NOT EXISTS(SELECT * FROM sys.tables WHERE name= 'Cardstack')
                                    CREATE TABLE Cardstack(
                                    CardstackId INT PRIMARY KEY IDENTITY NOT NULL,
                                    CardstackName NVARCHAR(50) NOT NULL UNIQUE
                                    )";
                connection.Execute(createStackTable);

                var createFlashcards = @"IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Flashcards')
                                   CREATE TABLE Flashcards(
                                   FlashcardId INT PRIMARY KEY IDENTITY NOT NULL,
                                   Question NVARCHAR(100) NOT NULL,
                                   Answer NVARCHAR(100) NOT NULL,
                                   StackId INT NOT NULL,
                                   CONSTRAINT fk_cardstack FOREIGN KEY(StackId)
                                   REFERENCES Cardstack(CardstackId)
                                   ON DELETE CASCADE
                                   ON UPDATE CASCADE
                                   )";
                connection.Execute(createFlashcards);
            }
        }
    }
}