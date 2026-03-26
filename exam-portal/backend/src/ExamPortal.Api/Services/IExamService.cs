using ExamPortal.Api.DTOs;

namespace ExamPortal.Api.Services;

public interface IExamService
{
    Task<SubmitExamResponseDto> SubmitAsync(SubmitExamRequestDto request);
    Task<ExamSubmissionResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<ExamSubmissionResponseDto>> GetAllAsync();
}