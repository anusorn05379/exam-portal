using ExamPortal.Api.Data;
using ExamPortal.Api.DTOs;
using ExamPortal.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.Api.Services;

public class ExamService : IExamService
{
    private readonly AppDbContext _context;

    public ExamService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SubmitExamResponseDto> SubmitAsync(SubmitExamRequestDto request)
    {
        var qustions = await _context.Questions
            .Include(q => q.Choices)
            .ToListAsync();

        var submission = new ExamSubmission
        {
            FullName = request.FullName,
            TotalQuestions = qustions.Count,
            SubmittedAt = DateTime.UtcNow,
        };

        int totalScore = 0;
        foreach (var answer in request.Answers)
        {
            var question = qustions.FirstOrDefault(c => c.Id == answer.QuestionId);

            if (question == null) continue;

            var selectedChoice = question.Choices.FirstOrDefault(c => c.Id == answer.ChoiceId);

            if (selectedChoice == null) continue;

            bool isCorrect = selectedChoice.IsCorrect;
            int score = isCorrect ? selectedChoice.Score : 0;

            totalScore += score;

            submission.Answers.Add(new ExamAnswer
            {
                QuestionId = answer.QuestionId,
                ChoiceId = answer.ChoiceId,
                IsCorrect = isCorrect,
                Score = score
            });
        }
        submission.Score = totalScore;
        _context.ExamSubmissions.Add(submission);
        await _context.SaveChangesAsync();

        return new SubmitExamResponseDto
        {
            SubmissionId = submission.Id,
            FullName = submission.FullName,
            TotalQuestions = submission.TotalQuestions,
            Score = submission.Score,
            Message = $"คุณ {submission.FullName} สอบได้คะแนน : {submission.Score}/{submission.TotalQuestions}"

        };

    }

    public async Task<ExamSubmissionResponseDto?> GetByIdAsync(int id)
    {
        var submission = await _context.ExamSubmissions
            .Include(e => e.Answers)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (submission == null) return null;
        return new ExamSubmissionResponseDto
        {
            SubmissionId = submission.Id,
            FullName = submission.FullName,
            TotalQuestions = submission.TotalQuestions,
            Score = submission.Score,
            SubmittedAt = submission.SubmittedAt,
            Answers = submission.Answers.Select(a => new ExamAnswerDto
            {
                QuestionId = a.QuestionId,
                ChoiceId = a.ChoiceId,
                IsCorrect = a.IsCorrect,
                Score = a.Score
            }).ToList()
        };
    }

    public async Task<IEnumerable<ExamSubmissionResponseDto>> GetAllAsync()
    {
        var submissions = await _context.ExamSubmissions
            .OrderByDescending(e => e.SubmittedAt)
            .ToListAsync();

        return submissions.Select(s => new ExamSubmissionResponseDto
        {
            SubmissionId = s.Id,
            FullName = s.FullName,
            Score = s.Score,
            TotalQuestions = s.TotalQuestions,
            SubmittedAt = s.SubmittedAt
        });
    }

}