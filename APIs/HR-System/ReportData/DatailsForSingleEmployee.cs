using System;
using System.Collections.Generic;
using System.Linq;
using HR_System.Models;

namespace HR_System.ReportData
{
    public partial class MainReport
    {
        //attendanceforAll
        public List<EmpDayDetails> NonUsualForSingleEmp(int id)
        {

            return this.NonUsualDays.Where(att => att.empId == id).ToList();
        }
    }
    public class EmpDetails
    {

    }

}
