namespace School.Application.DTOs.Attendance
{
    public class UpdateAttendanceRequest
    {
        public int StudentId { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}