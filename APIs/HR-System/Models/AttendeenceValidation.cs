using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
namespace HR_System.Models
{
    [ModelMetadataType(typeof(AttendeenceValidation))]
    public partial class Attendance
    { }
        public class AttendeenceValidation
        {
            public int EmpId { get; set; }
        
            [Required]
            public TimeSpan AttendingTime { get; set; }
        
            [Required]
            public TimeSpan LeavingTime { get; set; }
        
            [Required]
            public DateTime Day { get; set; }
        }
}
