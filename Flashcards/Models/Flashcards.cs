﻿namespace Flashcards.Models;

internal class FlashCards
{
    public int FlashcardId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int CardstackId { get; set; }
}

internal class FlashCardsDTO
{
    public int StackId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}