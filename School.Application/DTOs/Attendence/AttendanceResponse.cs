namespace School.Application.DTOs.Attendance
{
    public class AttendanceResponse
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}