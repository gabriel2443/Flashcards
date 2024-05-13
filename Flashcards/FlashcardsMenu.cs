using Flashcards.Database;
using Flashcards.Models;

namespace Flashcards
{
    internal class FlashcardsMenuUI
    {
        private FlashcardDatabaseManager flashcardDatabaseManager = new FlashcardDatabaseManager();

        internal void FlashCardsMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n\nSTACKS MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 Add a flashcard");
                Console.WriteLine("Type 2 to view stacks");
                Console.WriteLine("Type 3 to delete stacks");
                Console.WriteLine("Type 4 to update stacks");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddFlashcard();
                        break;
                }
            }
        }

        internal void AddFlashcard()
        {
            var cardStack = new CardStack();
            var flashcards = new FlashCards();
            Console.WriteLine("Please enter a the frond of the card");
            var inputFront = Console.ReadLine();
            flashcards.Question = inputFront;
            Console.WriteLine("Please enter the back of the card");
            var inputBack = Console.ReadLine();
            flashcards.Answer = inputBack;
            flashcards.StackId = cardStack.CardstackId;

            flashcardDatabaseManager.AddFlashard(flashcards);
        }
    }
}