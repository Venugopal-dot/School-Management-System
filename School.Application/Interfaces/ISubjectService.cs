using School.Application.DTOs.Subject;

namespace School.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();

        Task<SubjectDto?> GetByIdAsync(int id);

        Task<SubjectResponse> CreateAsync(CreateSubjectRequest request);

        Task<SubjectResponse> UpdateAsync(int id, UpdateSubjectRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
