using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly SchoolDbContext _context;

        public AttendanceRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Attendance> AddAsync(Attendance attendance)
        {
            attendance.CreatedOn = DateTime.UtcNow;

            _context.Attendances.Add(attendance);

            await _context.SaveChangesAsync();

            return attendance;
        }

        public async Task<Attendance> UpdateAsync(Attendance attendance)
        {
            attendance.UpdatedOn = DateTime.UtcNow;

            _context.Attendances.Update(attendance);

            await _context.SaveChangesAsync();

            return attendance;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (attendance == null)
                return false;

            attendance.IsDeleted = true;
            attendance.DeletedOn = DateTime.UtcNow;
            attendance.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AttendanceExistsAsync(
            int studentId,
            DateTime date)
        {
            return await _context.Attendances.AnyAsync(
                x => x.StudentId == studentId &&
                     x.Date.Date == date.Date &&
                     !x.IsDeleted);
        }

        public async Task<bool> AttendanceExistsForAnotherRecordAsync(
            int id,
            int studentId,
            DateTime date)
        {
            return await _context.Attendances.AnyAsync(
                x => x.Id != id &&
                     x.StudentId == studentId &&
                     x.Date.Date == date.Date &&
                     !x.IsDeleted);
        }
    }
}