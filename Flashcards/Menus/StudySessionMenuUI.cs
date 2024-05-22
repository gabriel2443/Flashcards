using Flashcards.Database;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.Menus;

internal class StudySessionMenuUI
{
    private StudySessionDatabase studySessionDatabase = new StudySessionDatabase();

    internal void StudySessionMenu()
    {
        var mainMenu = new MainMenuUI();
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

                case "Go back to main menu":
                    mainMenu.MainMenu();
                    break;

                case "View study session":
                    ViewStudySession();
                    break;
            }
        }
    }

    internal void StartSession()
    {
        var random = new Random();
        var stackDatabaseManager = new StackDatabaseManager();
        var flashDatabaseManager = new FlashcardDatabaseManager();
        var studySession = new StudySession();

        AnsiConsole.WriteLine("Please select a stack you want to study");

        var cardStacks = stackDatabaseManager.GetStacks();
        var select = new SelectionPrompt<CardStack>();
        select.Title("Select a stack");
        select.AddChoices(cardStacks);
        select.AddChoice(new CardStack { CardstackId = 0, CardstackName = "Go back to menu" });
        select.UseConverter(stackName => stackName.CardstackName);
        var selectedCardStack = AnsiConsole.Prompt(select);
        var getFlashcards = flashDatabaseManager.ReadFlashcardsDTO(selectedCardStack);
        int score = 0;

        for (int i = 0; i < getFlashcards.Count; i++)
        {
            studySession.DateStart = DateTime.Now;
            var randomFlashcard = random.Next(getFlashcards.Count);
            var flashcard = getFlashcards[randomFlashcard];
            AnsiConsole.WriteLine($"What is the answer to {flashcard.Question}");
            var answer = AnsiConsole.Prompt(new TextPrompt<string>("Please type your answer or press 0 to go back to study menu "));
            if (answer == "0") StudySessionMenu();
            if (answer != flashcard.Answer)
            {
                AnsiConsole.WriteLine($"You are incorrect, the answer to the quesstion is {flashcard.Answer}, press any key to show the next question or type 0 to go back to main menu");
                Console.ReadLine();
            }

            if (answer.ToLower() == flashcard.Answer.ToLower())
            {
                AnsiConsole.WriteLine("You are correct ");
                score++;
            };
        }

        var stackId = selectedCardStack.CardstackId;
        studySession.DateEnd = DateTime.Now;
        studySession.Score = score;
        studySession.StackId = stackId;
        studySessionDatabase.AddStudySession(studySession);
    }

    internal void ViewStudySession()
    {
        var studySessions = studySessionDatabase.GetStudySession();
        foreach (var session in studySessions)
        {
            var stackName = studySessionDatabase.GetStackName(session);
            var table = new Table();
            table.AddColumn(new TableColumn("Start Time").Centered());
            table.AddColumn(new TableColumn("End Time").Centered());
            table.AddColumn(new TableColumn("Score").Centered());
            table.AddRow("asdds", "lalal", "asdsada");
            AnsiConsole.Write(table);
        }
    }
}

/*$"{session.DateStart} {session.DateEnd} {session.Score} {stackName}"*/