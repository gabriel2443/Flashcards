using Flashcards.Database;
using Flashcards.Models;

namespace Flashcards
{
    internal class FlashcardsMenuUI
    {
        private FlashcardDatabaseManager flashcardDatabaseManager = new FlashcardDatabaseManager();
        private StackDatabaseManager stackDatabaseManager = new StackDatabaseManager();

        internal void FlashCardsMenu()
        {
            var mainMenu = new MainMenuUI();
            var stackMenu = new StackMenuUI();
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n\nSTACKS MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to go back to main menu");
                Console.WriteLine("Type 1 View stacks");
                Console.WriteLine("Type 2 to view flashcards");
                Console.WriteLine("Type 3 to delete stacks");
                Console.WriteLine("Type 4 to update stacks");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        mainMenu.MainMenu();
                        break;

                    case "1":
                        stackMenu.EditStack();
                        break;

                    case "2":
                        AddFlashcard();
                        break;
                        /*
                                            case "3":
                                                GetFlashcards();
                                                break;*/
                }
            }
        }

        internal void AddFlashcard()
        {
            var getStacks = new StackDatabaseManager();
            var stacksMenu = new StackMenuUI();
            stacksMenu.ReadStack();

            Console.WriteLine("Please select the number of the stack you want to add a flashcard");
            var stackNum = ValidateNumber.ValidateNum("Invalid input");

            var stackId = getStacks.GetStackById(Convert.ToInt32(stackNum));
            while (stackId == null || stackId.CardstackId != Convert.ToInt32(stackNum))
            {
                Console.WriteLine("stack number does not exist");
                stackNum = Console.ReadLine();
                stackId = getStacks.GetStackById(Convert.ToInt32(stackNum));
            }
            var flashcards = new FlashCards();
            Console.WriteLine("Please enter the front of the card");
            var inputFront = Console.ReadLine();
            flashcards.Question = inputFront;
            Console.WriteLine("Please enter the back of the card");
            var inputBack = Console.ReadLine();
            flashcards.Answer = inputBack;

            flashcards.CardstackId = stackId.CardstackId;

            flashcardDatabaseManager.AddFlashard(flashcards);
        }

        /*
                internal void GetFlashcards()
                {
                    Console.Clear();

                    var flashcards = flashcardDatabaseManager.ReadFlahcards();

                    if (flashcards.Any())
                    {
                        Console.WriteLine("List of stacks:");
                        foreach (var flashcard in flashcards)
                        {
                            Console.WriteLine($"{flashcard.FlashcardId}|| Question:{flashcard.Question} | Answer:{flashcard.Answer} ");
                        }
                    }
                    else { Console.WriteLine("No flashcards found"); };
                }
            }*/
    }
}