using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.ClassRoom;
using School.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomService _service;

        public ClassRoomController(IClassRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllClassRooms()
        {
            var classrooms = await _service.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<ClassRoomDto>>
            {
                Success = true,
                Message = "Class rooms retrieved successfully.",
                Data = classrooms
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoomById(int id)
        {
            var classroom = await _service.GetByIdAsync(id);

            return Ok(new ApiResponse<ClassRoomDto>
            {
                Success = true,
                Message = "Class room retrieved successfully.",
                Data = classroom
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateClassRoomAsync(
            CreateClassRoomRequest room)
        {
            var classroom = await _service.CreateAsync(room);

            return StatusCode(StatusCodes.Status201Created,
                new ApiResponse<ClassRoomResponse>
                {
                    Success = true,
                    Message = "Class room created successfully.",
                    Data = classroom
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateClassRoom(
            int id,
            UpdateClassRoomRequest room)
        {
            var classroom = await _service.UpdateAsync(id, room);

            return Ok(new ApiResponse<ClassRoomResponse>
            {
                Success = true,
                Message = "Class room updated successfully.",
                Data = classroom
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}