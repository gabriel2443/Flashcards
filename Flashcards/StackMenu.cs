using Flashcards.Models;

namespace Flashcards
{
    internal class StackMenuManager
    {
        private StackDatabaseManager stackDatabaseManager = new();

        internal void StackMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 Add a stack");
                Console.WriteLine("Type 2 to view stacks");

                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        isRunning = false;
                        Environment.Exit(0);
                        break;

                    case "1":
                        AddStack();
                        break;

                    case "2":
                        ReadStack();
                        break;
                }
            }
        }

        private void AddStack()
        {
            var cardStack = new CardStack();
            Console.WriteLine("Please enter the stack name which you want to add");

            var stack = Console.ReadLine();
            cardStack.StackName = stack;

            stackDatabaseManager.InsertStack(cardStack);
        }

        private void ReadStack()
        {
            var cardStacks = stackDatabaseManager.GetStacks();

            foreach (var stack in cardStacks)
            {
                Console.WriteLine($"{stack.StackName}");
            }
        }
    }
}