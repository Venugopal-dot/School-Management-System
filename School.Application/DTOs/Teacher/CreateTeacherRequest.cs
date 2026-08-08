using School.Domain.Enums;

namespace School.Application.DTOs.Teacher
{
    public class CreateTeacherRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;

        public string SubjectSpecialization { get; set; } = string.Empty;
        
    }
}
