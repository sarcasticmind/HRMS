using System;
using System.Collections.Generic;
using System.Linq;

namespace HR_System.ReportData
{
    public partial class MainReport
    {
        public void AttendingReport()
        {
            Holidays = _context.OfficialHolidays.Where(h => h.Date.Month == month && h.Date < DateTime.Now).Select(d => d.Date).ToList();
            AttForEmployee = attendanceforAll.GroupBy(att => att.EmpId).ToList()
                   .Select(g => new AttendenceReport(g.Key, g.Count())).ToList();
        }
        public List<DateTime> MonthWorrkingDays()
        {
            int range = DateTime.DaysInMonth(year, month);
            List<DateTime> Monthdays = Enumerable.Range(1, range)
                            .Select(day => new DateTime(year, month, day))
                            .ToList();
            List<DateTime> WeekEndDays =
                Monthdays.Where(day => day.ToString("dddd") == WeekEnd1
                    || day.ToString("dddd") == WeekEnd2
                ).ToList();
            List<DateTime> Vications = Holidays.Union(WeekEndDays).ToList();
            List<DateTime> WorkingDays = Monthdays.Except(Vications).ToList();
            return WorkingDays;
        }
    }

    public class AttendenceReport
    {
        public int empid { get; set; }
        public int nAttending { get; set; }
        public AttendenceReport(int empid, int n)
        {
            this.empid = empid;
            this.nAttending = n;
        }
    }

}
