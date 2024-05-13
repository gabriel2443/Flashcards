namespace Flashcards
{
    internal class MainMenuUI
    {
        private StackMenuUI stackMenu = new StackMenuUI();
        private FlashcardsMenuUI flashcardsMenu = new FlashcardsMenuUI();

        internal void MainMenu()
        {
            var isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to Manage Stacks");
                Console.WriteLine("Type 2 to Manage Flashcards");
                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        isRunning = false;
                        break;

                    case "1":
                        stackMenu.StackMenu();
                        break;

                    case "2":
                        flashcardsMenu.FlashCardsMenu();
                        break;
                }
            }
        }
    }
}