using School.Application.DTOs.ClassRoom;

namespace School.Application.Interfaces
{
    public interface IClassRoomService
    {
        Task<IEnumerable<ClassRoomDto>> GetAllAsync();
        Task<ClassRoomResponse> CreateAsync(CreateClassRoomRequest request);
        Task<ClassRoomResponse> UpdateAsync(int id, UpdateClassRoomRequest request);
        Task<bool> DeleteAsync(int id);
        Task<ClassRoomDto> GetByIdAsync(int id);

    }
}
