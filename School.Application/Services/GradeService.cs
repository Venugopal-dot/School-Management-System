using School.Application.DTOs.Grade;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GradeService(IGradeRepository gradeRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
    }

    private static void ValidateMarks(decimal marks)
    {
        if (marks < 0 || marks > 100)
        {
            throw new BusinessValidationException(
                "Marks must be between 0 and 100.");
        }
    }
    private static string CalculateGradeLetter(decimal marks)
    {
        if (marks >= 90)
            return "A";

        if (marks >= 70)
            return "B";

        if (marks >= 50)
            return "C";

        if (marks >= 35)
            return "D";

        return "F";
    }
    private static GradeDto MapToDto(Grade grade)
    {
        return new GradeDto
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            SubjectId = grade.SubjectId,
            Marks = grade.Marks,
            GradeLetter = grade.GradeLetter
        };
    }
    private static GradeResponse MapToResponse(Grade grade)
    {
        return new GradeResponse
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            SubjectId = grade.SubjectId,
            Marks = grade.Marks,
            GradeLetter = grade.GradeLetter
        };
    }

    public async Task<GradeResponse> CreateAsync(CreateGradeRequest request)
    {
        ValidateMarks(request.Marks);

        var student = await _studentRepository.GetByIdAsync(request.StudentId);

        if (student == null)
        {
            throw new NotFoundException($"Student with Id {request.StudentId} was not found.");
        }

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject == null)
        {
            throw new NotFoundException($"Subject with Id {request.SubjectId} was not found.");
        }

        if (await _gradeRepository.GradeExistsAsync(request.StudentId, request.SubjectId))
        {
            throw new DuplicateRecordException("A grade already exists for this student and subject.");
        }

        var gradeLetter = CalculateGradeLetter(request.Marks);

        var grade = new Grade
        {
            StudentId = request.StudentId,
            SubjectId = request.SubjectId,
            Marks = request.Marks,
            GradeLetter = gradeLetter
        };

        grade = await _gradeRepository.AddAsync(grade);

        return MapToResponse(grade);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _gradeRepository.DeleteAsync(id);
        
        if (!deleted)
        {
            throw new NotFoundException($"Grade with Id {id} was not found.");
        }

        return true;
    }

    public async Task<IEnumerable<GradeDto>> GetAllAsync()
    {
        var grades = await _gradeRepository.GetAllAsync();
        return grades.Select(MapToDto);
    }

    public async Task<GradeDto?> GetByIdAsync(int id)
    {
        var grade = await _gradeRepository.GetByIdAsync(id);
        if (grade == null)
        {
            throw new NotFoundException($"grade with Id {id} was not found.");
        }
        return MapToDto(grade);
    }

    public async Task<GradeResponse> UpdateAsync(int id, UpdateGradeRequest request)
    {
        var grade = await _gradeRepository.GetByIdAsync(id);

        if (grade == null)
        {
            throw new NotFoundException($"Grade with Id {id} was not found.");
        }

        ValidateMarks(request.Marks);

        var student = await _studentRepository.GetByIdAsync(request.StudentId);

        if (student == null)
        {
            throw new NotFoundException($"Student with Id {request.StudentId} was not found.");
        }

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject == null)
        {
            throw new NotFoundException($"Subject with Id {request.SubjectId} was not found.");
        }

        if (await _gradeRepository.GradeExistsForAnotherRecordAsync(id, request.StudentId, request.SubjectId))
        {
            throw new DuplicateRecordException("A grade already exists for this student and subject.");
        }

        grade.StudentId = request.StudentId;
        grade.SubjectId = request.SubjectId;
        grade.Marks = request.Marks;
        grade.GradeLetter = CalculateGradeLetter(request.Marks);

        grade = await _gradeRepository.UpdateAsync(grade);

        return MapToResponse(grade);
    }
}