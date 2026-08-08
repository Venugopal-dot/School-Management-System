using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllAsync();

        Task<Teacher?> GetByIdAsync(int id);

        Task<Teacher> AddAsync(Teacher teacher);

        Task<Teacher> UpdateAsync(Teacher teacher);

        Task<bool> DeleteAsync(int id);

        Task<bool> EmailExistsAsync(string email);

        Task<bool> EmailExistsForAnotherTeacherAsync(int id, string email);
    }
}
