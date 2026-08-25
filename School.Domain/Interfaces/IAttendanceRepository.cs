using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task<Attendance> AddAsync(Attendance attendance);

        Task<Attendance> UpdateAsync(Attendance attendance);

        Task<bool> DeleteAsync(int id);

        Task<bool> AttendanceExistsAsync(int studentId, DateTime date);

        Task<bool> AttendanceExistsForAnotherRecordAsync(
            int id,
            int studentId,
            DateTime date);
    }
}