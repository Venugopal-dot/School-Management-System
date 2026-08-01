namespace School.Application.DTOs.Student;

public class StudentResponse
{
    public int Id { get; set; }

    public string AdmissionNumber { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}