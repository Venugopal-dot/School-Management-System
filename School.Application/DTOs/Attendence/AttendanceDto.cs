namespace School.Application.DTOs.Attendance
{
    public class AttendanceDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}