using School.Application.DTOs.Subject;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Common;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }
        private static void ValidateSubjectName(string subjectName)
        {
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                throw new BusinessValidationException(
                    "Subject name is required.");
            }
        }
        private static SubjectDto MapToDto(Subject subject)
        {
            return new SubjectDto
            {
                Id = subject.Id,
                SubjectCode=subject.SubjectCode,
                SubjectName = subject.SubjectName,
                Description = subject.Description,
                IsActive = subject.IsActive
            };
        }
        private static SubjectResponse MapToResponse(Subject subject)
        {
            return new SubjectResponse
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                Description = subject.Description,
                SubjectCode = subject.SubjectCode,
                IsActive = subject.IsActive 
            };
        }

        public async Task<SubjectResponse> CreateAsync(CreateSubjectRequest request)
        {
            ValidateSubjectName(request.SubjectName);

            if (await _repository.SubjectNameExistsAsync(request.SubjectName))
            {
                throw new DuplicateRecordException(
                    $"Subject name '{request.SubjectName}' already exists.");
            }

            var subject = new Subject
            {
                SubjectName = request.SubjectName,
                Description = request.Description
            };

            subject = await _repository.AddAsync(subject);
            subject.SubjectCode = SubjectCodeGenerator.Generate(subject.Id);
            subject = await _repository.UpdateAsync(subject);
            return MapToResponse(subject);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException($"Subject with Id {id} was not found.");
            }
            return true;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            var subjects = await _repository.GetAllAsync();
            return subjects.Select(MapToDto);
        }

        public async Task<SubjectDto?> GetByIdAsync(int id)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
            {
                throw new NotFoundException($"subject with id {id} was not found");
            }
            return MapToDto(subject);
        }

        public async Task<SubjectResponse> UpdateAsync(
    int id,
    UpdateSubjectRequest request)
        {
            var subject = await _repository.GetByIdAsync(id);

            if (subject == null)
            {
                throw new NotFoundException(
                    $"Subject with Id {id} was not found.");
            }

            ValidateSubjectName(request.SubjectName);

            if (await _repository.SubjectNameExistsForAnotherRecordAsync(
                    id,
                    request.SubjectName))
            {
                throw new DuplicateRecordException(
                    $"Subject name '{request.SubjectName}' already exists.");
            }

            subject.SubjectName = request.SubjectName;
            subject.Description = request.Description;
            subject.IsActive = request.IsActive;

            subject = await _repository.UpdateAsync(subject);

            return MapToResponse(subject);
        }
    }
}
