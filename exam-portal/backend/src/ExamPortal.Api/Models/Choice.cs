namespace ExamPortal.Api.Models;

public class Choice
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int Score { get; set; } = 1;

    public Question? Question { get; set; }
}