using HR_System.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using HR_System.ReportData;

namespace HR_System.ReportData
{
    public partial class MainReport
    {
        HR_SystemContext _context = new HR_SystemContext();
        int year;
        List<DateTime> Holidays;
        int month;
        public int numOfMonthlyWorkingDay;
        public List<ExtraPenalityReport> TotalExtraPenality;
        public List<EmpDayDetails> NonUsualDays { get; set; }
            = new List<EmpDayDetails>();
        List<Attendance> attendanceforAll;
        List<AttendenceReport> AttForEmployee;
        string WeekEnd1;
        string WeekEnd2;
        public MainReport(List<Attendance> attendance, int year, int month)
        {
            WeekEnd1 = _context.WeekEnds.FirstOrDefault().Weekend1;
            WeekEnd2 = _context.WeekEnds.FirstOrDefault().Weekend2;
            this.attendanceforAll = attendance;
            this.year = year;
            this.month = month;
            numOfMonthlyWorkingDay = (_context.WeekEnds.FirstOrDefault().Weekend2 == null) ? 26 : 22;
            this.NonUsualDays = attendance.Where(att => att.AttendingTime != att.Emp.AttendingTime
                                                        || att.LeavingTime != att.Emp.LeavingTime)
                                           .Select(att => new EmpDayDetails(att.EmpId, att.Emp.Name, att.Emp.Salary, att.Emp.AttendingTime, att.Emp.LeavingTime, att.AttendingTime, att.LeavingTime)).ToList();
            AttendingReport();
            ExtraPenalityHoursReport();
        }
        public double totalsalary(int id)
        {
            HR_SystemContext _context = new HR_SystemContext();
            Employee emp = _context.Employees.FirstOrDefault(Emp => Emp.Id == id);
            double ext = (TotalExtraPenality.FirstOrDefault(x => x.id == id) == null) ? 0
                        : TotalExtraPenality.FirstOrDefault(x => x.id == id).extraInPounds;
            double pen = (TotalExtraPenality.FirstOrDefault(x => x.id == id) == null) ? 0
                        : TotalExtraPenality.FirstOrDefault(x => x.id == id).PenalityInPounds;
            int Absence = AttForEmployee.FirstOrDefault(x => x.empid == id) == null ? MonthWorrkingDays().Count()
                        : MonthWorrkingDays().Count() - AttForEmployee.FirstOrDefault(x => x.empid == id).nAttending;
            return (double)emp.Salary + ext - pen - (Absence * (double)emp.Salary / MonthWorrkingDays().Count());
        }
        public List<View> Reportview()
        {
            List<View> SalaryReport = new();
            foreach (var emp in _context.Employees)
            {
                {
                    View v = new()
                    {
                        Name = emp.Name,
                        salary = (double)emp.Salary,
                        FinalSalary = totalsalary(emp.Id),
                        NumberOfAttendingDays = AttForEmployee
                        .FirstOrDefault(a => a.empid == emp.Id) == null ?
                         0 : AttForEmployee.FirstOrDefault(a => a.empid == emp.Id).nAttending,
                        NumberOfAbsanceDays = AttForEmployee
                        .FirstOrDefault(a => a.empid == emp.Id) == null ?
                        MonthWorrkingDays().Count() : MonthWorrkingDays().Count() - AttForEmployee
                        .FirstOrDefault(a => a.empid == emp.Id).nAttending,
                        ExtraInhours = TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id) == null ?
                                0 : TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id).extraInHours,
                        penalityInhours = TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id) == null ?
                                0 : TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id).PenalityInHours,
                        ExtraInPounds = TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id) == null ?
                                0 : TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id).extraInPounds,
                        PenaityInPunds = TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id) == null ?
                                0 : TotalExtraPenality.FirstOrDefault(e => e.id == emp.Id).PenalityInPounds,
                    };
                    SalaryReport.Add(v);
                }
            }
            return SalaryReport;
        }
    }
}