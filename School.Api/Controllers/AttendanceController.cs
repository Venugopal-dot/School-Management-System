using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.Attendance;
using School.Application.Interfaces;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attendances = await _service.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<AttendanceDto>>
            {
                Success = true,
                Message = "Attendance records retrieved successfully.",
                Data = attendances
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var attendance = await _service.GetByIdAsync(id);

            return Ok(new ApiResponse<AttendanceDto>
            {
                Success = true,
                Message = "Attendance record retrieved successfully.",
                Data = attendance
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance(
            CreateAttendanceRequest request)
        {
            var attendance = await _service.CreateAsync(request);

            return StatusCode(StatusCodes.Status201Created,
                new ApiResponse<AttendanceResponse>
                {
                    Success = true,
                    Message = "Attendance created successfully.",
                    Data = attendance
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(
            int id,
            UpdateAttendanceRequest request)
        {
            var attendance = await _service.UpdateAsync(id, request);

            return Ok(new ApiResponse<AttendanceResponse>
            {
                Success = true,
                Message = "Attendance updated successfully.",
                Data = attendance
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}