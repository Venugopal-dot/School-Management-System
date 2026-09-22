using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.Teacher;
using School.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;
        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllAsync()
        {
            var teachers = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<TeacherDto>>
            {
                Success = true,
                Message = "Teachers retrived successfully.",
                Data = teachers
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            var teacher = await _service.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetTeacherById),
                new { id = teacher.Id },
                new ApiResponse<TeacherResponse>
                {
                    Success = true,
                    Message = "Teacher created successfully.",
                    Data = teacher
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _service.GetByIdAsync(id);

            return Ok(new ApiResponse<TeacherDto>
            {
                Success = true,
                Message = "Teachers retrieved successfully.",
                Data = teacher
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherRequest request)
        {
            var teacher = await _service.UpdateAsync(id, request);

            return Ok(new ApiResponse<TeacherResponse>
            {
                Success = true,
                Message = "Teacher updated successfully.",
                Data = teacher
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
