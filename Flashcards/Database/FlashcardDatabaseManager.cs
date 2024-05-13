using Dapper;
using Flashcards.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Flashcards.Database
{
    internal class FlashcardDatabaseManager
    {
        private string connectionStr = ConfigurationManager.AppSettings.Get("ConnectionString");

        internal void AddFlashard(FlashCards flashCard)
        {
            var sql = $"INSERT INTO Flashcards VALUES(@Question, @Answer, @StackId)";
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Execute(sql, flashCard);
            }
        }
    }
}