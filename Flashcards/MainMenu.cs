using Spectre.Console;

namespace Flashcards
{
    internal class MainMenuUI
    {
        private StackMenuUI stackMenu = new StackMenuUI();
        private FlashcardsMenuUI flashcardsMenu = new FlashcardsMenuUI();

        internal void MainMenu()
        {
            Console.Clear();
            var isRunning = true;

            while (isRunning)
            {
                var select = new SelectionPrompt<string>();
                select.Title("What would you like to do");
                select.AddChoice("Close Application");
                select.AddChoice("Manage Stacks");
                select.AddChoice("Manage Flashcards");
                var userInput = AnsiConsole.Prompt(select);

                switch (userInput)
                {
                    case "Close Application":
                        isRunning = false;
                        Environment.Exit(0);
                        break;

                    case "Manage Stacks":
                        stackMenu.StackMenu();
                        break;

                    case "Manage Flashcards":
                        flashcardsMenu.StackSelection();
                        break;
                }
            }
        }
    }
}