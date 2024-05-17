using Flashcards.Database;
using Flashcards.Menus;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.FlashcardsMenu
{
    internal class FlashcardsMenuUI
    {
        private FlashcardDatabaseManager flashcardDatabaseManager = new FlashcardDatabaseManager();
        private StackDatabaseManager stackDatabaseManager = new StackDatabaseManager();
        private CardStack stack = new CardStack();

        internal void FlashCardsMenu(CardStack stack)

        {
            var mainMenu = new MainMenuUI();
            bool isRunning = true;
            while (isRunning)
            {
                var select = new SelectionPrompt<string>();
                select.Title("\nFLASHCARDS MENU\n\n");
                select.AddChoice("Go back to main menu");
                select.AddChoice("View flashcards");
                select.AddChoice("Add flashcard");
                select.AddChoice("Update a flash card");
                select.AddChoice("Delete a flashcard");
                var input = AnsiConsole.Prompt(select);
                switch (input)
                {
                    case "Go back to main menu":
                        mainMenu.MainMenu();
                        break;

                    case "Add flashcard":
                        AddFlashCard();
                        break;

                    case "View flashcards":
                        ViewFlashcards(stack);
                        break;

                    case "Update a flash card":
                        UpdateFlashcards(stack);
                        break;

                    case "Delete a flashcard":
                        DeleteFlashcard(stack);
                        break;
                }
            }
        }

        internal void StackSelection()
        {
            var cardStacks = stackDatabaseManager.GetStacks();

            var select = new SelectionPrompt<CardStack>();
            select.Title("Select a stack");
            select.AddChoices(cardStacks);
            select.AddChoice(new CardStack { CardstackId = 0, CardstackName = "Go back to menu" });
            select.UseConverter(stackName => stackName.CardstackName);

            var selectedCardStack = AnsiConsole.Prompt(select);
            FlashCardsMenu(selectedCardStack);
            ViewFlashcards(selectedCardStack);
            UpdateFlashcards(selectedCardStack);
            DeleteFlashcard(selectedCardStack);
        }

        internal void AddFlashCard()
        {
            Console.Clear();
            var stacks = new StackDatabaseManager();
            var stackId = stacks.GetStackById();

            if (stackId.CardstackId == 0)
            {
                Console.WriteLine("No stacks found");
                return;
            }

            var flashcards = new FlashCards();
            var inputFront = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the front of the card"));
            var inputBack = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the back of the card"));

            flashcards.CardstackId = Convert.ToInt32(stackId.CardstackId);

            flashcards.Answer = inputBack;
            flashcards.Question = inputFront;

            flashcardDatabaseManager.AddFlashard(flashcards);
        }

        internal void ViewFlashcards(CardStack stack)
        {
            Console.Clear();
            var getFlashcards = flashcardDatabaseManager.ReadFlahcards(stack);

            int id = 1;

            if (getFlashcards.Count() == 0) Console.WriteLine("No flashcards found");
            else
            {
                Console.WriteLine("List of flashcards:");
                foreach (var flashcard in getFlashcards)
                {
                    AnsiConsole.Write(new Rows(
                    new Text($"{id++}\t {flashcard.Question} \t {flashcard.Answer}")));
                }
            }
        }

        internal void UpdateFlashcards(CardStack stack)
        {
            var getFlashcards = flashcardDatabaseManager.ReadFlahcards(stack);

            var select = new SelectionPrompt<FlashCards>();
            select.AddChoices(getFlashcards);
            select.UseConverter(flashcardName => $"{flashcardName.Question} {flashcardName.Answer}");
            var selectedFlashcard = AnsiConsole.Prompt(select);

            var inputFront = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the updated front of the card"));

            var inputBack = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the updated back of the card"));

            flashcardDatabaseManager.UpdateFlashcards(selectedFlashcard, inputFront, inputBack);
        }

        internal void DeleteFlashcard(CardStack stack)
        {
            var getFlashcards = flashcardDatabaseManager.ReadFlahcards(stack);

            var select = new SelectionPrompt<FlashCards>();
            select.AddChoices(getFlashcards);
            select.UseConverter(flashcardName => $"{flashcardName.Question} {flashcardName.Answer}");
            var selectedFlashcard = AnsiConsole.Prompt(select);
            flashcardDatabaseManager.DeleteFlashcard(selectedFlashcard);
        }
    }
}