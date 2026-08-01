using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class ClassRoomRepository : IClassRoomRepository
    {
        private readonly SchoolDbContext _context;
        public ClassRoomRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClassRoom>> GetAllAsync()
        {
            return await _context.ClassRooms
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<ClassRoom?> GetByIdAsync(int id)
        {
            return await _context.ClassRooms
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<ClassRoom> AddAsync(ClassRoom classRoom)
        {
            classRoom.CreatedOn = DateTime.Now;
            _context.ClassRooms.Add(classRoom);
            await _context.SaveChangesAsync();
            return classRoom;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _context.ClassRooms
                .FirstOrDefaultAsync(x => x.Id == id);

            if (room == null)
                return false;

            room.IsDeleted = true;
            room.DeletedOn = DateTime.UtcNow;
            room.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<ClassRoom> UpdateAsync(ClassRoom classRoom)
        {
            classRoom.UpdatedOn = DateTime.Now;
            _context.ClassRooms.Update(classRoom);
            await _context.SaveChangesAsync();
            return classRoom;
        }

        public async Task<bool> ClassRoomExistsAsync(string className, string section, string academicYear)
        {
            return await _context.ClassRooms.AnyAsync(
                x => x.ClassName == className && 
                x.Section == section && 
                x.AcademicYear == academicYear && 
                !x.IsDeleted);
        }

        public async Task<bool> ClassRoomExistsForAnotherRecordAsync(int id, string className, string section, string academicYear)
        {
            return await _context.ClassRooms.AnyAsync(x => x.Id != id &&
            x.ClassName == className &&
            x.Section == section &&
            x.AcademicYear == academicYear &&
            !x.IsDeleted);
        }
    }
}
