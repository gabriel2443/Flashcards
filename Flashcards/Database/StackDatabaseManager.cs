using Dapper;
using Flashcards.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Flashcards.Database;

public class StackDatabaseManager
{
    private string connectionStr = ConfigurationManager.AppSettings.Get("ConnectionString");

    internal void InsertStack(CardStack stack)
    {
        using (var connection = new SqlConnection(connectionStr))
        {
            var insertStack = $@"INSERT INTO Cardstack (CardstackName) VALUES ('{stack.CardstackName}')";

            connection.Execute(insertStack);
        }
    }

    internal List<CardStack> GetStacks()
    {
        using (var connection = new SqlConnection(connectionStr))
        {
            var sql = @"SELECT * FROM Cardstack";

            var readStacks = connection.Query<CardStack>(sql).ToList();
            return readStacks;
        }
    }

    internal CardStack GetStackById(int cardStackId)
    {
        var sql = @"SELECT CardstackId FROM Cardstack WHERE CardstackId = @CardstackId";
        using (var connection = new SqlConnection(connectionStr))
        {
            var getId = connection.ExecuteScalar<int>(sql, new { CardstackId = cardStackId });
            return new CardStack { CardstackId = getId };
        }
    }

    internal void UpdateStack(CardStack cardStack)
    {
        using (var connection = new SqlConnection(connectionStr))
        {
            var sql = $@"UPDATE Cardstack SET CardstackName = '{cardStack.CardstackName}' WHERE CardstackId = {cardStack.CardstackId}";

            connection.Execute(sql, cardStack);
        }
    }

    internal void DeleteStack(CardStack cardStack)
    {
        using (var connection = new SqlConnection(connectionStr))
        {
            var sql = $@"DELETE FROM Cardstack WHERE CardstackId = {cardStack.CardstackId} ";
            connection.Execute(sql, cardStack);
        }
    }
}