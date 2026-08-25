namespace School.Application.DTOs.Attendance
{
    public class CreateAttendanceRequest
    {
        public int StudentId { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}