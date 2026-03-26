using ExamPortal.Api.Data;
using ExamPortal.Api.DTOs;
using ExamPortal.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamsController : ControllerBase
{
    private readonly IExamService _examService;

    public ExamsController(IExamService examService)
    {
        _examService = examService;
    }

    [HttpPost("submit")]
    public async Task<ActionResult<SubmitExamResponseDto>> Submit([FromBody] SubmitExamRequestDto request)
    {
        if (string.IsNullOrEmpty(request.FullName))
        {
            return BadRequest("FullName is required.");
        }
        if (request.Answers == null || !request.Answers.Any())
        {
            return BadRequest("At least one answer is required.");
        }
        var result = await _examService.SubmitAsync(request);

        return Ok(result);

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _examService.GetByIdAsync(id);
        if (result == null)
            return NotFound("Exam result not found");

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _examService.GetAllAsync();
        return Ok(results);
    }
}