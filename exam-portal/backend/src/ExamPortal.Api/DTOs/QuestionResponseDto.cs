namespace ExamPortal.Api.DTOs;

public class QuestionResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int OrderNo { get; set; }
    public List<ChoiceResponseDto> Choices { get; set; } = new();
}