using Flashcards.Database;
using Flashcards.Models;

namespace Flashcards
{
    internal class StackMenuUI
    {
        private StackDatabaseManager stackDatabaseManager = new();

        internal void StackMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n\nSTACKS MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 Add a stack");
                Console.WriteLine("Type 2 to view stacks");
                Console.WriteLine("Type 3 to delete stacks");
                Console.WriteLine("Type 4 to update stacks");

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

                    case "3":
                        DeleteStack();
                        break;

                    case "4":
                        UpdateStack();
                        break;
                }
            }
        }

        private void AddStack()
        {
            var cardStack = new Models.CardstackDTO();
            Console.WriteLine("Please enter the stack name which you want to add");

            var stack = Console.ReadLine();
            cardStack.CardstackName = stack;

            stackDatabaseManager.InsertStack(cardStack);
        }

        private void ReadStack()
        {
            Console.Clear();
            var cardStacks = stackDatabaseManager.GetStacks();

            if (cardStacks.Any())
            {
                Console.WriteLine("List of stacks:");
                foreach (var card in cardStacks)
                {
                    Console.WriteLine($"Stack Number: {card.CardstackId}, Stack Name: {card.CardstackName}");
                }
            }
            else
            {
                Console.WriteLine("No stacks found.");
            }
        }

        private void UpdateStack()
        {
            var cardStack = new CardStack();
            ReadStack();

            Console.WriteLine("Please enter the number of the stack you want to edit");
            var inputId = Console.ReadLine();

            while (!Int32.TryParse(inputId, out _) || Convert.ToInt32(inputId) < 0)
            {
                Console.WriteLine("Please enter a valid number");

                inputId = Console.ReadLine();
            }

            cardStack.CardstackId = Convert.ToInt32(inputId);

            Console.WriteLine("Please enter the updated name for the stack");
            var inputName = Console.ReadLine();
            while (string.IsNullOrEmpty(inputName))
            {
                Console.WriteLine("Please enter a valid name");
                inputName = Console.ReadLine();
            }
            cardStack.CardstackName = inputName;

            stackDatabaseManager.UpdateStack(cardStack);
        }

        private void DeleteStack()
        {
            ReadStack();
            var cardStacks = new CardStack();

            Console.WriteLine("Please enter the number of the stack you want to delete");
            var input = Console.ReadLine();

            while (!Int32.TryParse(input, out _) || Convert.ToInt32(input) < 0)
            {
                Console.WriteLine("Please enter a valid number");
                input = Console.ReadLine();
            }

            cardStacks.CardstackId = Convert.ToInt32(input);

            stackDatabaseManager.DeleteStack(cardStacks);
        }
    }
}