using School.Domain.Common;

namespace School.Domain.Entities;

public class Subject:BaseEntity
{
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
