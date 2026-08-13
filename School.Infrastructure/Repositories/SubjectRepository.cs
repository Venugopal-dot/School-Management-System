using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolDbContext _context;
        public SubjectRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Subject> AddAsync(Subject subject)
        {
            subject.CreatedOn = DateTime.UtcNow;

            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if(subject == null)
            {
                return false;
            }

            subject.IsDeleted = true;
            subject.DeletedOn = DateTime.UtcNow;
            subject.UpdatedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<bool> SubjectNameExistsAsync(string subjectName)
        {
            return await _context.Subjects.AnyAsync(x => x.SubjectName == subjectName && !x.IsDeleted);
        }

        public async Task<bool> SubjectNameExistsForAnotherRecordAsync(int id, string subjectName)
        {
            return await _context.Subjects.AnyAsync(
                x => x.Id != id && 
                x.SubjectName == subjectName && 
                !x.IsDeleted);
        }
            
        public async Task<Subject> UpdateAsync(Subject subject)
        {
            subject.UpdatedOn = DateTime.UtcNow;
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return subject;
        }
    }
}
