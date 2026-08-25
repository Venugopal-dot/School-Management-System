using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDbContext _context;
        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            teacher.CreatedOn = DateTime.UtcNow;

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            
            return teacher;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (teacher == null)
            {
                return false;
            }
            teacher.IsDeleted = true;
            teacher.DeletedOn = DateTime.UtcNow;
            teacher.UpdatedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Teachers.AnyAsync(
                x => x.Email == email && 
                !x.IsDeleted);
        }

        public async Task<bool> EmailExistsForAnotherTeacherAsync(int id, string email)
        {
            return await _context.Teachers.AnyAsync(
                x =>
                x.Id != id &&
                x.Email == email &&
                !x.IsDeleted);
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            return await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Teacher> UpdateAsync(Teacher teacher)
        {
            teacher.UpdatedOn = DateTime.UtcNow;
            _context.Teachers.Update(teacher);
            
            await _context.SaveChangesAsync();
            return teacher;
        }
    }
}
