using School.Domain.Common;

namespace School.Domain.Entities;

public class ClassRoom:BaseEntity
{
    public string ClassName { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;

    public string AcademicYear { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}