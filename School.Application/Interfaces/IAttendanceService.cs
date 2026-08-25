using School.Application.DTOs.Attendance;

namespace School.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync();

        Task<AttendanceDto> GetByIdAsync(int id);

        Task<AttendanceResponse> CreateAsync(CreateAttendanceRequest request);

        Task<AttendanceResponse> UpdateAsync(
            int id,
            UpdateAttendanceRequest request);

        Task<bool> DeleteAsync(int id);
    }
}