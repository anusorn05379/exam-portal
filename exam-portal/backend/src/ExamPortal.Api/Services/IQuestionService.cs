using ExamPortal.Api.DTOs;

public interface IQuestionService
{
    Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync();
}
