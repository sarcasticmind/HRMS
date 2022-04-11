using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
namespace HR_System.Models
{
    [ModelMetadataType(typeof(OfficialHolidaysValidation))]
    public partial class OfficialHoliday
    { 
    }
    public class OfficialHolidaysValidation
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public DateTime Date { get; set; }
    }
}
