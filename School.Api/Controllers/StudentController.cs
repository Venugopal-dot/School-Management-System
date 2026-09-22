using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.Student;
using School.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentservice;
        public StudentController(IStudentService studentservice)
        {
            _studentservice = studentservice;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentservice.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<StudentDto>>
            {
                Success = true,
                Message = "Students retrieved successfully.",
                Data = students
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest request)
        {
            var student = await _studentservice.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = student.Id },
                new ApiResponse<StudentResponse>
                {
                    Success = true,
                    Message = "Student created successfully.",
                    Data = student
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentservice.GetByIdAsync(id);

            return Ok(new ApiResponse<StudentDto>
            {
                Success = true,
                Message = "Student retrieved successfully.",
                Data = student
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentRequest request)
        {
            var student = await _studentservice.UpdateAsync(id, request);

            return Ok(new ApiResponse<StudentResponse>
            {
                Success = true,
                Message = "Student updated successfully.",
                Data = student
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentservice.DeleteAsync(id);

            return NoContent();
        }
    }
}
