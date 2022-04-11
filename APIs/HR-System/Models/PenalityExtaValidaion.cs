using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HR_System.Models
{

    [ModelMetadataType(typeof(PenalityExtaValidaion))]
    public partial class PenalityExtra
    { }

    public class PenalityExtaValidaion
    {
        [Required]
        [Range(1, 10)]
        public double Penalityhour { get; set; }
        
        [Required]
        [Range(1, 10)]
        public double Extrahour { get; set; }
    }
}
