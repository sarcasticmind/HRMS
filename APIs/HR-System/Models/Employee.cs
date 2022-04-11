using System;
using System.Collections.Generic;

#nullable disable

namespace HR_System.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfContract { get; set; }
        public decimal Salary { get; set; }
        public TimeSpan AttendingTime { get; set; }
        public TimeSpan LeavingTime { get; set; }
        public double? ExtraHour { get; set; }
        public double? PenaltyHour { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
