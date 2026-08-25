using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly SchoolDbContext _context;
        public GradeRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Grade> AddAsync(Grade grade)
        {
            grade.CreatedOn = DateTime.UtcNow;
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return grade;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return false;
            }

            grade.IsDeleted = true;
            grade.DeletedOn = DateTime.UtcNow;
            grade.UpdatedOn = DateTime.UtcNow;

            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _context.Grades.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Grade?> GetByIdAsync(int id)
        {
            return await _context.Grades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        }

        public async Task<bool> GradeExistsAsync(int studentId, int subjectId)
        {
            return await _context.Grades.AnyAsync
            (
                x => x.StudentId == studentId && 
                x.SubjectId == subjectId && 
                !x.IsDeleted
            );
        }

        public async Task<bool> GradeExistsForAnotherRecordAsync(int id, int studentId, int subjectId)
        {
            return await _context.Grades.AnyAsync
            (
                x => x.StudentId == studentId && 
                x.SubjectId == subjectId && 
                !x.IsDeleted && 
                x.Id != id
            );
        }

        public async Task<Grade> UpdateAsync(Grade grade)
        {
            grade.UpdatedOn = DateTime.UtcNow;
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
            return grade;
        }
    }
}
