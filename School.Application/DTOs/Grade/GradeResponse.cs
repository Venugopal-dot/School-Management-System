namespace School.Application.DTOs.Grade;

public class GradeResponse
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public decimal Marks { get; set; }

    public string GradeLetter { get; set; } = string.Empty;
}