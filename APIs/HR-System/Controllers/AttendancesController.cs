using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_System.Models;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly HR_SystemContext _context;

        public AttendancesController(HR_SystemContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances()
        {
            return await _context.Attendances
                    .OrderBy(attendce => attendce.Day).ToListAsync();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        [HttpGet("Filter/{StartDay}")]
        public ActionResult<List<Attendance>> GetFilteredAttendance(DateTime StartDay, DateTime EndDay, string name)
        {
            if (EndDay < StartDay)
                return BadRequest();
            List<Attendance> RequiredAttendance;


            if (name != null)
                RequiredAttendance = _context.Attendances.Where
                    (att => att.Day >= StartDay && att.Day <= EndDay && att.Emp.Name == name)
                    .ToList();
            else
                RequiredAttendance = _context.Attendances.Where
                    (att => att.Day >= StartDay && att.Day <= EndDay)
                    .ToList();

            if (RequiredAttendance == null)
                return NotFound();

            return RequiredAttendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.Id || !validAtteendence(attendance))
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
            if (!validAtteendence(attendance) || !uniq(attendance))
            {
                return BadRequest();
            }
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }

        private bool uniq (Attendance atten)
        {
            bool cnd = _context.Attendances.
                    FirstOrDefault(att => att.Day == atten.Day && att.EmpId == atten.EmpId) == null;
            return cnd;
        }
        private bool validAtteendence(Attendance attendance)
        {
            bool cnd1 = attendance.Day <= DateTime.Now;
            bool cnd2 = attendance.AttendingTime < attendance.LeavingTime;
            bool cnd3 = _context.OfficialHolidays.FirstOrDefault
                            (holy => holy.Date == attendance.Day) == null;
            bool cnd4 = attendance.Day > _context.Employees.FirstOrDefault
                        (em => em.Id == attendance.EmpId).DateOfContract;
            bool cnd5 = _context.WeekEnds.FirstOrDefault
                            (w => w.Weekend1 == attendance.Day.ToString("dddd")
                             || w.Weekend2 == attendance.Day.ToString("dddd")) == null;
          return
                cnd1 && cnd2 && cnd3 && cnd4 && cnd5;
        }
    }
}
