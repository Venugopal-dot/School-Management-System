using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly SchoolDbContext _context;

    public StudentRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.AsNoTracking().Where(s => !s.IsDeleted).ToListAsync(); 
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
    }

    public async Task<Student> AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return student;
    }

    public async Task<Student> UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();

        return student;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return false;

        student.IsDeleted = true;
        student.DeletedOn = DateTime.UtcNow;
        student.UpdatedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Students.AnyAsync(x => x.Email == email && !x.IsDeleted);
    }
        
    public async Task<bool> EmailExistsForAnotherStudentAsync(int studentId, string email)
    {
        return await _context.Students.AnyAsync(s => s.Email == email && s.Id != studentId && !s.IsDeleted);
    }
}