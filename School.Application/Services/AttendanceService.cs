using School.Application.DTOs.Attendance;
using School.Application.Exceptions;
using School.Application.Interfaces;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentRepository _studentRepository;

        public AttendanceService(
            IAttendanceRepository attendanceRepository,
            IStudentRepository studentRepository)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
        }

        private static void ValidateDate(DateTime date)
        {
            if (date.Date > DateTime.UtcNow.Date)
            {
                throw new BusinessValidationException(
                    "Attendance date cannot be in the future.");
            }
        }

        private static AttendanceDto MapToDto(Attendance attendance)
        {
            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                Date = attendance.Date,
                IsPresent = attendance.IsPresent
            };
        }

        private static AttendanceResponse MapToResponse(Attendance attendance)
        {
            return new AttendanceResponse
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                Date = attendance.Date,
                IsPresent = attendance.IsPresent
            };
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();

            return attendances.Select(MapToDto);
        }

        public async Task<AttendanceDto> GetByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
            {
                throw new NotFoundException(
                    $"Attendance with Id {id} was not found.");
            }

            return MapToDto(attendance);
        }

        public async Task<AttendanceResponse> CreateAsync(
            CreateAttendanceRequest request)
        {
            ValidateDate(request.Date);

            var student = await _studentRepository
                .GetByIdAsync(request.StudentId);

            if (student == null)
            {
                throw new NotFoundException(
                    $"Student with Id {request.StudentId} was not found.");
            }

            if (await _attendanceRepository.AttendanceExistsAsync(
                request.StudentId,
                request.Date))
            {
                throw new DuplicateRecordException(
                    "Attendance already exists for this student and date.");
            }

            var attendance = new Attendance
            {
                StudentId = request.StudentId,
                Date = request.Date.Date,
                IsPresent = request.IsPresent
            };

            attendance = await _attendanceRepository.AddAsync(attendance);

            return MapToResponse(attendance);
        }

        public async Task<AttendanceResponse> UpdateAsync(
            int id,
            UpdateAttendanceRequest request)
        {
            ValidateDate(request.Date);

            var attendance = await _attendanceRepository
                .GetByIdAsync(id);

            if (attendance == null)
            {
                throw new NotFoundException(
                    $"Attendance with Id {id} was not found.");
            }

            var student = await _studentRepository
                .GetByIdAsync(request.StudentId);

            if (student == null)
            {
                throw new NotFoundException(
                    $"Student with Id {request.StudentId} was not found.");
            }

            if (await _attendanceRepository
                .AttendanceExistsForAnotherRecordAsync(
                    id,
                    request.StudentId,
                    request.Date))
            {
                throw new DuplicateRecordException(
                    "Attendance already exists for this student and date.");
            }

            attendance.StudentId = request.StudentId;
            attendance.Date = request.Date.Date;
            attendance.IsPresent = request.IsPresent;

            attendance = await _attendanceRepository.UpdateAsync(attendance);

            return MapToResponse(attendance);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _attendanceRepository.DeleteAsync(id);

            if (!deleted)
            {
                throw new NotFoundException(
                    $"Attendance with Id {id} was not found.");
            }

            return true;
        }
    }
}