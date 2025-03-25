namespace HRM.API.Domain.DTOs.Attendance
{
    public class FindAttendanceDTO
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime date { get; set; } = DateTime.Now;
    }
}
