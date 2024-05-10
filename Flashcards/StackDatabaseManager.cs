using Dapper;
using Flashcards.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Flashcards;

public class StackDatabaseManager
{
    private string connectionStr = ConfigurationManager.AppSettings.Get("ConnectionString");

    internal void InsertStack(CardStack stack)
    {
        using (var connection = new SqlConnection(connectionStr))
        {
            connection.Open();
            var insertStack = $@"INSERT INTO Cardstack (name) VALUES ('{stack.StackName}')";

            connection.Execute(insertStack);
        }
    }

    internal List<CardStack> GetStacks()
    {
        using (var connection = new SqlConnection(connectionStr))
        {
            var sql = @"SELECT * FROM Cardstack";
            var cardStacks = connection.Query<CardStack>(sql);
            return cardStacks.ToList();
        }
    }
}