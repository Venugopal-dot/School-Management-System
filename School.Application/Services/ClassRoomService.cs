using School.Application.DTOs.ClassRoom;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services
{
    public class ClassRoomService : IClassRoomService
    {
        private readonly IClassRoomRepository _repository;
        public ClassRoomService(IClassRoomRepository repository)
        {
            _repository = repository;
        }
        private static void ValidateClassRoom(string className, string section)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new BusinessValidationException("Class Name is required.");
            }

            if (string.IsNullOrWhiteSpace(section))
            {
                throw new BusinessValidationException("Section is required.");
            }
        }
        private static void ValidateAcademicYear(string academicYear)
        {
            if (string.IsNullOrWhiteSpace(academicYear))
            {
                throw new BusinessValidationException("Academic Year is required.");
            }
            
            var years = academicYear.Split('-');

            if (years.Length != 2)
            {
                throw new BusinessValidationException("Academic Year must be in the format YYYY-YYYY.");
            }

            if (!int.TryParse(years[0], out int startYear) ||
                !int.TryParse(years[1], out int endYear))
            {
                throw new BusinessValidationException("Academic Year is invalid.");
            }

            if (endYear != startYear + 1)
            {
                throw new BusinessValidationException("Academic Year is invalid.");
            }
        }
        private static ClassRoomDto MapToDto(ClassRoom classRoom)
        {
            return new ClassRoomDto
            {
                Id = classRoom.Id,
                ClassName = classRoom.ClassName,
                Section = classRoom.Section,
                AcademicYear = classRoom.AcademicYear
            };
        }
        private static ClassRoomResponse MapToResponse(ClassRoom classRoom)
        {
            return new ClassRoomResponse
            {
                Id = classRoom.Id,
                ClassName = classRoom.ClassName,
                Section = classRoom.Section,
                AcademicYear = classRoom.AcademicYear
            };
        }

        public async Task<IEnumerable<ClassRoomDto>> GetAllAsync()
        {
            var classrooms = await _repository.GetAllAsync();
            return classrooms.Select(MapToDto);
        }

        public async Task<ClassRoomDto> GetByIdAsync(int id)
        {
            var classroom = await _repository.GetByIdAsync(id);
            if (classroom == null)
            {
                throw new NotFoundException($"ClassRoom with Id {id} was not found.");
            }
            return MapToDto(classroom);
        }
        public async Task<ClassRoomResponse> CreateAsync(CreateClassRoomRequest request)
        {
            ValidateAcademicYear(request.AcademicYear);
            ValidateClassRoom(request.ClassName, request.Section);
            ValidateAcademicYear(request.AcademicYear);
            if (await _repository.ClassRoomExistsAsync(
                request.ClassName,
                request.Section,
                request.AcademicYear))
            {
                throw new DuplicateRecordException("ClassRoom already exists.");
            }
            
            var classroom = new ClassRoom
            {
                ClassName = request.ClassName,
                AcademicYear = request.AcademicYear,
                Section = request.Section,
            };
            classroom = await _repository.AddAsync(classroom);
            return MapToResponse(classroom);
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException($"ClassRoom with Id {id} was not found.");
            }
            return true;
        }

        public async Task<ClassRoomResponse> UpdateAsync(int id, UpdateClassRoomRequest request)
        {
            var classroom = await _repository.GetByIdAsync(id);
            ValidateClassRoom(request.ClassName, request.Section);
            ValidateAcademicYear(request.AcademicYear);
            if (classroom == null)
            {
                throw new NotFoundException($"ClassRoom with this id {id} was not found");
            }
            if (await _repository.ClassRoomExistsForAnotherRecordAsync(id, request.ClassName, request.Section, request.AcademicYear))
            {
                throw new DuplicateRecordException("ClassRoom already exists.");
            }

            ValidateAcademicYear(request.AcademicYear);

            classroom.AcademicYear = request.AcademicYear;
            classroom.ClassName = request.ClassName;
            classroom.Section = request.Section;

            classroom = await _repository.UpdateAsync(classroom);
            return MapToResponse(classroom);
        }
    }
}
