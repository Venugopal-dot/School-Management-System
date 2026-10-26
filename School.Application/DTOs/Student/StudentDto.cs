using School.Domain.Enums;

namespace School.Application.DTOs.Student;

public class StudentDto
{
    public int Id { get; set; }

    public string AdmissionNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int ClassRoomId { get; set; }
}