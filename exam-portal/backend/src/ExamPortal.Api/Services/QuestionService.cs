using ExamPortal.Api.Data;
using ExamPortal.Api.DTOs;
using Microsoft.EntityFrameworkCore;

public class QuestionService : IQuestionService
{
    private readonly AppDbContext _context;

    public QuestionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync()
    {
        var questions = await _context.Questions
            .AsNoTracking()
            .Include(q => q.Choices)
            .ToListAsync();

        return questions.Select(q => new QuestionResponseDto
        {
            Id = q.Id,
            Text = q.Text,
            OrderNo = q.OrderNo,
            Choices = q.Choices.Select(c => new ChoiceResponseDto
            {
                Id = c.Id,
                Text = c.Text
            }).ToList()
        }).ToList();
    }
}
