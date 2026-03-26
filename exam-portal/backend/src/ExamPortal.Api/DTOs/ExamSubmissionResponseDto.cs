namespace ExamPortal.Api.DTOs;

public class ExamSubmissionResponseDto
{
    public int SubmissionId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime SubmittedAt { get; set; }
    public List<ExamAnswerDto> Answers { get; set; } = new();
}

public class ExamAnswerDto
{
    public int QuestionId { get; set; }
    public int ChoiceId { get; set; }
    public bool IsCorrect { get; set; }
    public int Score { get; set; }
}
