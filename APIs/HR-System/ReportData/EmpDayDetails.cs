using System;
namespace HR_System.ReportData
{
    public class EmpDayDetails
    {
        public int empId { get; set; }
        public string EmpName { set; get; }
        public decimal BaseSalary { get; set; }
        public TimeSpan? BaseAttendingTime { get; set; }
        public TimeSpan? BaseLeavingTime { get; set; }
        public TimeSpan? DayAttendingTime { get; set; }
        public TimeSpan? DayLeavingTime { get; set; }
        private double AttendingDeifference { get; set; }
        private double LeavingDeifference { get; set; }

        public double DayExtraHour;
        public double DayPenalityHour;
        public EmpDayDetails()
        {
        }
        public EmpDayDetails(int id, string EmpName, decimal BaseSalary, TimeSpan BaseAttendingTime, TimeSpan BaseLeavingTime, TimeSpan? DayAttendingTime, TimeSpan? DayLeavingTime)
        {
            this.empId = id;
            this.EmpName = EmpName;
            this.BaseSalary = BaseSalary;
            this.DayAttendingTime = DayAttendingTime;
            this.DayLeavingTime = DayLeavingTime;
            this.BaseAttendingTime = BaseAttendingTime;
            this.BaseLeavingTime = BaseLeavingTime;
            this.AttendingDeifference = AttendingDeifferenceCalc();
            this.LeavingDeifference = LeavingDeifferenceCalc();
            CalcExtra();
            CalcPenality();
        }
        private double AttendingDeifferenceCalc() => (this.BaseAttendingTime - this.DayAttendingTime).Value.TotalHours;
        private double LeavingDeifferenceCalc() => (this.DayLeavingTime - this.BaseLeavingTime).Value.TotalHours;
        public double CalcExtra()
        {
            this.DayExtraHour += Math.Max(0, this.LeavingDeifference);
            this.DayExtraHour += Math.Max(0, this.AttendingDeifference);
            return this.DayExtraHour;
        }
        public double CalcPenality()
        {
            this.DayPenalityHour += -1 * Math.Min(0, AttendingDeifference);
            this.DayPenalityHour += -1 * Math.Min(0, LeavingDeifference);
            return DayPenalityHour;
        }
    }
}
