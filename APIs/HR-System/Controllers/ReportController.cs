using HR_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using HR_System.ReportData;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly HR_SystemContext _context;

        public ReportController(HR_SystemContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<List<View>> getAll(int year, int month)
        {
            List<Attendance> attendance = _context.Attendances.Include("Emp").Where(att => att.Day.Month == month && att.Day.Year == year).ToList();
            MainReport Report = new MainReport(attendance, year, month);
            return Report.Reportview();
        }


        [HttpGet("detail")]
        public List<EmpDayDetails> getdetail(int year, int month, int id)
        {
            List<Attendance> attendance = _context.Attendances.Include("Emp").Where(att => att.Day.Month == month).ToList();
            MainReport Report1 = new MainReport(attendance, year, month);
            return Report1.NonUsualForSingleEmp(id);
        }

    }
}
