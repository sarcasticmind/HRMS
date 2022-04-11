using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
namespace HR_System.Models
{
    [ModelMetadataType(typeof(EmployeeValidation))]
    public partial class Employee
    { }
        public class EmployeeValidation
    {
        [Required]
        [MinLength(3)]
        [MaxLength(70)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(120)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^01[0|1|2|5]{1}[0-9]{8}$")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string Gender { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        [RegularExpression(@"^[2|3]\d{13}")]
        public string NationalId { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public DateTime DateOfContract { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public TimeSpan AttendingTime { get; set; }
        
        [Required]
        public TimeSpan LeavingTime { get; set; }

        [Range(0, 10)]
        public double? ExtraHour { get; set; }
        
        [Range(0, 10)]
        public double? PenaltyHour { get; set; }
        public int Id { get; set; }
    }
}
