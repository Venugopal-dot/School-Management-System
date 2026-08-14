namespace School.Application.DTOs.Grade;

public class UpdateGradeRequest
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public decimal Marks { get; set; }
}