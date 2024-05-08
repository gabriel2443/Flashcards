using Flashcards;

var databaseController = new DatabaseController();
databaseController.CreateDatabase();
databaseController.DeleteStack();
Console.WriteLine("Db created");