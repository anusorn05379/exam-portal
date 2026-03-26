namespace ExamPortal.Api.DTOs;

public class SubmitExamRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public List<ExamAnswerRequestDto> Answers { get; set; } = new();
}