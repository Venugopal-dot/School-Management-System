using Microsoft.AspNetCore.Mvc;
using School.Api.Models;
using School.Application.DTOs.ClassRoom;
using School.Application.Interfaces;

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
        public async Task<IActionResult> CreateClassRoomAsync(CreateClassRoomRequest room)
        {
            var classroom = await _service.CreateAsync(room);
            return Ok(new ApiResponse<ClassRoomResponse> {
                Success = true,
                Message = "Class room created successfully.",
                Data = classroom
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassRoom(int id, UpdateClassRoomRequest room)
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
        public async Task<IActionResult> DeleteClassRoom(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Class room deleted successfully.",
                Data = deleted
            });
        }
    }
}
