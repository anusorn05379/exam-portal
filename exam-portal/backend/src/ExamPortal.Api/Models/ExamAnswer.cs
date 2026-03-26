namespace ExamPortal.Api.Models;

public class ExamAnswer
{
    public int Id { get; set; }
    public int ExamSubmissionId { get; set; }
    public int QuestionId { get; set; }
    public int ChoiceId { get; set; }
    public bool IsCorrect { get; set; }
    public int Score { get; set; }

    public ExamSubmission? ExamSubmission { get; set; }
}