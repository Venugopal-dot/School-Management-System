using School.Domain.Entities;

namespace School.Domain.Interfaces
{
    public interface IClassRoomRepository
    {
        Task<IEnumerable<ClassRoom>> GetAllAsync();

        Task<ClassRoom?> GetByIdAsync(int id);

        Task<ClassRoom> AddAsync(ClassRoom classRoom);

        Task<ClassRoom> UpdateAsync(ClassRoom classRoom);

        Task<bool> DeleteAsync(int id);

        Task<bool> ClassRoomExistsAsync(string className, string section, string academicYear);

        Task<bool> ClassRoomExistsForAnotherRecordAsync(int id, string className, string section, string academicYear);

    }
}
