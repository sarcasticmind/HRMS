using System;
using System.Collections.Generic;

#nullable disable

namespace HR_System.Models
{
    public partial class OfficialHoliday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
