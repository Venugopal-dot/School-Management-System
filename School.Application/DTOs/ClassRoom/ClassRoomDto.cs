namespace School.Application.DTOs.ClassRoom
{
    public class ClassRoomDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;

        public string Section { get; set; } = string.Empty;

        public string AcademicYear { get; set; } = string.Empty;
    }
}
