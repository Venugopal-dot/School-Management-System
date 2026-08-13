namespace School.Application.DTOs.Subject
{
    public class UpdateSubjectRequest
    {
        public string SubjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
