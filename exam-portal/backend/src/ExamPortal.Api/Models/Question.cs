namespace ExamPortal.Api.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int OrderNo { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Choice> Choices { get; set; } = new();
}