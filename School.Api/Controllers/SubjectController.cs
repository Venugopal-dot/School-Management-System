using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.Subject;
using School.Application.Interfaces;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subjects = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<SubjectDto>>
            {
                Success = true,
                Message = "Subjects are retrieved successfully",
                Data = subjects
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _service.GetByIdAsync(id);

            return Ok(new ApiResponse<SubjectDto>
            {
                Success = true,
                Message = "Subject retrieved successfully.",
                Data = subject
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequest request)
        {
            var subject = await _service.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetSubjectById),
                new { id = subject.Id },
                new ApiResponse<SubjectResponse>
                {
                    Success = true,
                    Message = "Subject Created Successfully",
                    Data = subject
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSubjectRequest request)
        {
            var subject = await _service.UpdateAsync(id, request);

            return Ok(new ApiResponse<SubjectResponse>
            {
                Success = true,
                Message = "Subject updated successfully.",
                Data = subject
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
