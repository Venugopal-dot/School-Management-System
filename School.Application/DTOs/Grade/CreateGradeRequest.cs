namespace School.Application.DTOs.Grade;

public class CreateGradeRequest
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public decimal Marks { get; set; }
}