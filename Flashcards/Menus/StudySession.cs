using Flashcards.Database;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.Menus
{
    internal class StudySession
    {
        internal void StudySessionMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                var select = new SelectionPrompt<string>();
                select.Title("\nStudy session MENU\n\n");
                select.AddChoice("Go back to main menu");
                select.AddChoice("Start study session");
                select.AddChoice("View study session");
                var selectedOption = AnsiConsole.Prompt(select);

                switch (selectedOption)
                {
                    case "Start study session":
                        StartSession();
                        break;
                }
            }
        }

        internal void StartSession()
        {
            var random = new Random();
            var stackDatabaseManager = new StackDatabaseManager();
            var flashDatabaseManager = new FlashcardDatabaseManager();
            var flashcards = new FlashCardsDTO();
            AnsiConsole.WriteLine("Please select a stack you want to study");
            var cardStacks = stackDatabaseManager.GetStacks();

            var select = new SelectionPrompt<CardStack>();
            select.Title("Select a stack");
            select.AddChoices(cardStacks);
            select.AddChoice(new CardStack { CardstackId = 0, CardstackName = "Go back to menu" });
            select.UseConverter(stackName => stackName.CardstackName);
            var selectedCardStack = AnsiConsole.Prompt(select);
            var getFlashcards = flashDatabaseManager.ReadFlahcards(selectedCardStack);

            for (int i = 0; i < getFlashcards.Count; i++)
            {
                var randomFlashcard = random.Next(getFlashcards.Count);

                var answer = Console.ReadLine();

                if (answer != flashcards.Answer) { AnsiConsole.WriteLine($"The answer to the quesstion was{flashcards.Answer}"); }
            }
        }
    }
}