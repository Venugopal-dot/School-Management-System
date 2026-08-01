using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Infrastructure.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options):base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
