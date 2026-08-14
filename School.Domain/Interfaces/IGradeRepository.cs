using School.Domain.Entities;

namespace School.Domain.Interfaces;

public interface IGradeRepository
{
    Task<IEnumerable<Grade>> GetAllAsync();

    Task<Grade?> GetByIdAsync(int id);

    Task<Grade> AddAsync(Grade grade);

    Task<Grade> UpdateAsync(Grade grade);

    Task<bool> DeleteAsync(int id);

    Task<bool> GradeExistsAsync(int studentId, int subjectId);

    Task<bool> GradeExistsForAnotherRecordAsync
    (
        int id,
        int studentId,
        int subjectId
    );
}