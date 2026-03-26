namespace ExamPortal.Api.DTOs;

public class SubmitExamResponseDto
{
    public int SubmissionId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public string Message { get; set; } = string.Empty;
}