using HR_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR_System.ReportData
{
    public partial class MainReport
    {
        public void ExtraPenalityHoursReport()
        {
            this.TotalExtraPenality = NonUsualDays.GroupBy(att => att.empId)
                   .Select(g => new ExtraPenalityReport(id: g.Key, g.Sum(s => s.DayExtraHour), g.Sum(s => s.DayPenalityHour)))
                   .ToList();
        }
    }
    public class ExtraPenalityReport
    {

        public int id { get; set; }
        public double sumOfExtraHours { get; set; }
        public double extraInHours { get; set; }
        public TimeSpan? attendingTime { get; set; }
        public TimeSpan? leavigTime { get; set; }
        public double extraInPounds { get; set; }
        public double sumOfPenalityHours { get; set; }
        public double PenalityInHours { get; set; }
        public double PenalityInPounds { get; set; }
        public ExtraPenalityReport(int id, double sumOfExtraHours, double sumOfPenalityHours)
        {
            this.id = id;
            this.sumOfExtraHours = sumOfExtraHours;
            HR_SystemContext _context = new HR_SystemContext();
            Employee emp = _context.Employees.FirstOrDefault(Emp => Emp.Id == id);

            if (emp != null)
            {
                double? exrtaValue = emp.ExtraHour != null ? emp.ExtraHour :
                       _context.PenalityExtras.FirstOrDefault().Extrahour;

                attendingTime = emp.AttendingTime;
                leavigTime = emp.LeavingTime;

                double HourlySalary = ((double)emp.Salary / 30) /
                    (this.leavigTime - this.attendingTime).Value.TotalHours;

                extraInHours = (double)(exrtaValue) * this.sumOfExtraHours;
                extraInPounds = extraInHours * HourlySalary;

                double? penalityValue = emp.PenaltyHour != null ? emp.PenaltyHour :
                 _context.PenalityExtras.FirstOrDefault().Penalityhour;

                this.sumOfPenalityHours = sumOfPenalityHours;

                this.PenalityInHours = (double)(penalityValue) * this.sumOfPenalityHours;
                PenalityInPounds = PenalityInHours * HourlySalary;
            }
        }
    }
}
