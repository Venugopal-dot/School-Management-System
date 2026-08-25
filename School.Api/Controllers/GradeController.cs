using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.Grade;
using School.Application.Interfaces;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController : ControllerBase
{
    private readonly IGradeService _service;

    public GradeController(IGradeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var grades = await _service.GetAllAsync();

        return Ok(new ApiResponse<IEnumerable<GradeDto>>
        {
            Success = true,
            Message = "Grades retrieved successfully.",
            Data = grades
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGradeById(int id)
    {
        var grade = await _service.GetByIdAsync(id);

        return Ok(new ApiResponse<GradeDto>
        {
            Success = true,
            Message = "Grade retrieved successfully.",
            Data = grade
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateGradeRequest request)
    {
        var grade = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetGradeById),
            new { id = grade.Id },
            new ApiResponse<GradeResponse>
            {
                Success = true,
                Message = "Grade created successfully.",
                Data = grade
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateGradeRequest request)
    {
        var grade = await _service.UpdateAsync(id, request);

        return Ok(new ApiResponse<GradeResponse>
        {
            Success = true,
            Message = "Grade updated successfully.",
            Data = grade
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrade(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}