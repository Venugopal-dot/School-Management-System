using School.Domain.Common;

namespace School.Domain.Entities;

public class Teacher : BaseEntity
{
    public string EmployeeCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public DateTime JoiningDate { get; set; }

    public string Qualification { get; set; } = string.Empty;

    public string SubjectSpecialization { get; set; } = string.Empty;
}