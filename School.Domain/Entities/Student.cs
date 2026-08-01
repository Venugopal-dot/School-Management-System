using School.Domain.Common;
using School.Domain.Enums;

namespace School.Domain.Entities;

public class Student : BaseEntity
{

    public string AdmissionNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; } 

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    // Foreign Key
    public int ClassRoomId { get; set; }

    // Navigation Property
    public ClassRoom? ClassRoom { get; set; }
}