using School.Application.DTOs.Grade;

namespace School.Application.Interfaces;

public interface IGradeService
{
    Task<IEnumerable<GradeDto>> GetAllAsync();

    Task<GradeDto?> GetByIdAsync(int id);

    Task<GradeResponse> CreateAsync(CreateGradeRequest request);

    Task<GradeResponse> UpdateAsync(int id, UpdateGradeRequest request);

    Task<bool> DeleteAsync(int id);
}