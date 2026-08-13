using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task<Subject> AddAsync(Subject subject);
        Task<Subject> UpdateAsync(Subject subject);
        Task<bool> DeleteAsync(int id);
        Task<bool> SubjectNameExistsAsync(string subjectName);
        Task<bool> SubjectNameExistsForAnotherRecordAsync(int id, string subjectName);
    }
}
