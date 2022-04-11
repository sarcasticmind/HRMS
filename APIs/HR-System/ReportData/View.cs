namespace HR_System.ReportData
{
    public class View
    {
        public string Name { get; set; }
        public double salary { get; set; }
        public int NumberOfAttendingDays { get; set; }
        public int NumberOfAbsanceDays { get; set; }
        public double ExtraInhours { get; set; }
        public double penalityInhours { get; set; }
        public double ExtraInPounds { get; set; }
        public double PenaityInPunds { get; set; }
        public double FinalSalary { get; set; }
        public View()
        {

        }

        public View(string Name, double salary, int NumberOfAttendingDays, int NumberOfAbsanceDays, double ExtraInhours, double penalityInhours, double ExtraInPounds, double PenaityInPunds, double FinalSalary)
        {
            this.Name = Name;
            this.salary = salary;
            this.NumberOfAttendingDays = NumberOfAttendingDays;
            this.NumberOfAbsanceDays = NumberOfAbsanceDays;
            this.ExtraInhours = ExtraInhours;
            this.penalityInhours = penalityInhours;
            this.ExtraInPounds = ExtraInPounds;
            this.PenaityInPunds = PenaityInPunds;
            this.FinalSalary = FinalSalary;
        }
    }
}
