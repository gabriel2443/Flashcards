using Flashcards;
using Flashcards.Database;

var databaseCreation = new DatabaseCreation();
var mainMenu = new MainMenuUI();

databaseCreation.CreateDatabase();
mainMenu.MainMenu();