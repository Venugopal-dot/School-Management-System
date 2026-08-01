using School.Domain.Entities;

namespace School.Domain.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(int id);

    Task<Student> AddAsync(Student student);

    Task<Student> UpdateAsync(Student student);

    Task<bool> DeleteAsync(int id);

    Task<bool> EmailExistsAsync(string email);

    Task<bool> EmailExistsForAnotherStudentAsync(int studentId, string email);
}