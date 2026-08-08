using School.Application.DTOs.Student;
using School.Application.DTOs.Teacher;

namespace School.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();

        Task<TeacherDto?> GetByIdAsync(int id);

        Task<TeacherResponse> CreateAsync(CreateTeacherRequest request);

        Task<TeacherResponse> UpdateAsync(int id, UpdateTeacherRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
