using School.Domain.Common;

namespace School.Domain.Entities;

public class Subject:BaseEntity
{
    //public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
