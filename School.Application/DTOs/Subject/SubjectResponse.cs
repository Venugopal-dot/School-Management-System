namespace School.Application.DTOs.Subject
{
    public class SubjectResponse
    {
        public int Id { get; set; }

        public string SubjectCode { get; set; } = string.Empty;

        public string SubjectName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
