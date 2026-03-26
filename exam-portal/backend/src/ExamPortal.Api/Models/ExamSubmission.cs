namespace ExamPortal.Api.Models;

public class ExamSubmission
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public List<ExamAnswer> Answers { get; set; } = new();
}