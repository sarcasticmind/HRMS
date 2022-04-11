using System;
using System.Collections.Generic;

#nullable disable

namespace HR_System.Models
{
    public partial class Attendance
    {
        public int Id { get; set; }
        public TimeSpan AttendingTime { get; set; }
        public TimeSpan LeavingTime { get; set; }
        public int EmpId { get; set; }
        public DateTime Day { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
