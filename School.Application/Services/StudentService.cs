using School.Application.DTOs.Student;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Common;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    private static void ValidateStudentAge(DateTime dateOfBirth)
    {
        if (dateOfBirth > DateTime.Today)
        {
            throw new BusinessValidationException("Date of birth cannot be in the future.");
        }

        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        if (age < 6)
        {
            throw new BusinessValidationException("Student must be at least 6 years old.");
        }

        if (age > 17)
        {
            throw new BusinessValidationException("Student age cannot exceed 17 years.");
        }
    }
    private static StudentResponse MapToResponse(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            AdmissionNumber = student.AdmissionNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email
        };
    }
    private static StudentDto MapToDto(Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            AdmissionNumber = student.AdmissionNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email
        };
    }
    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return students.Select(MapToDto);
    }
    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
        {
            throw new NotFoundException($"Student with Id {id} was not found.");
        }
        return MapToDto(student);
    }
    public async Task<StudentResponse> CreateAsync(CreateStudentRequest request)
    {
        if (await _repository.EmailExistsAsync(request.Email))
        {
            throw new DuplicateRecordException("Email already exists.");
        }

        ValidateStudentAge(request.DateOfBirth);        

        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            ClassRoomId = request.ClassRoomId
        };
        student = await _repository.AddAsync(student);
        student.AdmissionNumber = AdmissionNumberGenerator.Generate(student.Id);
        student = await _repository.UpdateAsync(student);

        return MapToResponse(student);
    }

    public async Task<StudentResponse> UpdateAsync(int id, UpdateStudentRequest request)
    {
        var student = await _repository.GetByIdAsync(id);

        if (student == null)
        {
            throw new NotFoundException($"Student with Id {id} was not found.");
        }

        if (await _repository.EmailExistsForAnotherStudentAsync(id, request.Email))
        {
            throw new DuplicateRecordException("Email already exists.");
        }

        ValidateStudentAge(request.DateOfBirth);

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.DateOfBirth = request.DateOfBirth;
        student.Gender = request.Gender;
        student.Email = request.Email;
        student.PhoneNumber = request.PhoneNumber;
        student.Address = request.Address;
        student.ClassRoomId = request.ClassRoomId;

        student = await _repository.UpdateAsync(student);

        return MapToResponse(student);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _repository.DeleteAsync(id);

        if (!deleted)
        {
            throw new NotFoundException($"Student with Id {id} was not found.");
        }

        return true;
    }
}