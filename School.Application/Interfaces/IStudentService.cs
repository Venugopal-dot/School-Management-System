using School.Application.DTOs.Student;

namespace School.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();

    Task<StudentDto?> GetByIdAsync(int id);

    Task<StudentResponse> CreateAsync(CreateStudentRequest request);

    Task<StudentResponse> UpdateAsync(int id, UpdateStudentRequest request);

    Task<bool> DeleteAsync(int id);
}