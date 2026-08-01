namespace School.Application.DTOs.ClassRoom;

public class ClassRoomResponse
{
    public int Id { get; set; }

    public string ClassName { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;
    
    public string AcademicYear { get; set; } = string.Empty;
}