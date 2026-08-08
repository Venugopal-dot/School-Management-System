using School.Application.DTOs.Teacher;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Common;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;
        public TeacherService(ITeacherRepository repository)
        {
            _repository = repository;
        }
        private static void ValidateJoiningDate(DateTime joiningDate)
        {
            if (joiningDate > DateTime.Today)
            {
                throw new BusinessValidationException("Joining date cannot be in the future.");
            }
        }
        private static void ValidateTeacherAge(DateTime dateOfBirth)
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

            if (age < 21)
            {
                throw new BusinessValidationException("Teacher must be at least 21 years old.");
            }
            if (age > 60)
            {
                throw new BusinessValidationException("Teacher must be 60 years old or younger.");
            }
        }

        private static TeacherResponse MapToResponse(Teacher teacher)
        {
            return new TeacherResponse
            {
                Id = teacher.Id,
                EmployeeCode = teacher.EmployeeCode,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }

        private static TeacherDto MapToDto(Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                EmployeeCode = teacher.EmployeeCode,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            var teachers = await _repository.GetAllAsync();
            return teachers.Select(MapToDto);
        }

        public async Task<TeacherDto?> GetByIdAsync(int id)
        {
            var teacher = await _repository.GetByIdAsync(id);
            if (teacher == null)
            {
                throw new NotFoundException($"Teacher with id {id} not found ");
            }
            return MapToDto(teacher);
        }
        public async Task<TeacherResponse> CreateAsync(CreateTeacherRequest request)
        {
            if(await _repository.EmailExistsAsync(request.Email))
            {
                throw new DuplicateRecordException("Email alredy exists");
            }

            ValidateTeacherAge(request.DateOfBirth);
            ValidateJoiningDate(request.JoiningDate);

            var teacher = new Teacher
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                SubjectSpecialization = request.SubjectSpecialization,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Qualification = request.Qualification,
                JoiningDate = request.JoiningDate
            };
            teacher = await _repository.AddAsync(teacher);
            teacher.EmployeeCode = EmployeeCodeGenerator.Generate(teacher.Id);
            teacher = await _repository.UpdateAsync(teacher);

            return MapToResponse(teacher);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException($"Teacher with Id {id} was not found.");
            }
            return true;
        }        

        public async Task<TeacherResponse> UpdateAsync(int id, UpdateTeacherRequest request)
        {
            var teacher = await _repository.GetByIdAsync(id);

            if (teacher == null)
            {
                throw new NotFoundException($"Teacher with id {id} not found");
            }
            if(await _repository.EmailExistsForAnotherTeacherAsync(id,request.Email))
            {
                throw new DuplicateRecordException($"Email already exists with another employee");
            }

            ValidateTeacherAge(request.DateOfBirth);
            ValidateJoiningDate(request.JoiningDate);

            teacher.FirstName = request.FirstName;
            teacher.LastName = request.LastName;
            teacher.DateOfBirth = request.DateOfBirth;
            teacher.Email = request.Email;
            teacher.PhoneNumber = request.PhoneNumber;
            teacher.Qualification = request.Qualification;
            teacher.JoiningDate = request.JoiningDate;
            teacher.SubjectSpecialization = request.SubjectSpecialization;

            teacher = await _repository.UpdateAsync(teacher);

            return MapToResponse(teacher);
        }
    }
}
