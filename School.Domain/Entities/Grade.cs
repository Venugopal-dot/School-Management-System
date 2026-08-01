using School.Domain.Common;

namespace School.Domain.Entities;

public class Grade : BaseEntity
{
    //public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public decimal Marks { get; set; }

    public string GradeLetter { get; set; } = string.Empty;

    public Student? Student { get; set; }

    public Subject? Subject { get; set; }
}