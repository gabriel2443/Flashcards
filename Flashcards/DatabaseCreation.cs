using Dapper;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Flashcards
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
                                    cardstack_id INT PRIMARY KEY IDENTITY NOT NULL,
                                    name NVARCHAR(50) NOT NULL UNIQUE
                                    )";
                connection.Execute(createStackTable);

                var createFlashcards = @"IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Flashcards')
                                   CREATE TABLE Flashcards(
                                   flashcard_id INT PRIMARY KEY IDENTITY NOT NULL,
                                   question NVARCHAR(100) NOT NULL,
                                   answer NVARCHAR(100) NOT NULL,
                                   stack_id INT NOT NULL,
                                   CONSTRAINT fk_cardstack FOREIGN KEY(stack_id)
                                   REFERENCES Cardstack(cardstack_id)
                                   ON DELETE CASCADE
                                   ON UPDATE CASCADE
                                   )";
                connection.Execute(createFlashcards);
            }
        }
    }
}