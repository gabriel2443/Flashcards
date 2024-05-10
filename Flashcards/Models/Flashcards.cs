namespace Flashcards.Models;

internal class FlashCards
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int StackId { get; set; }
}

internal class FlashCardsDTO
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;

    public int StackId { get; set; }
}