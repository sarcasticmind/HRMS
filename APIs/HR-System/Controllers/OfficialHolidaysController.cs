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
    public class OfficialHolidaysController : ControllerBase
    {
        private readonly HR_SystemContext _context;

        public OfficialHolidaysController(HR_SystemContext context)
        {
            _context = context;
        }

        // GET: api/OfficialHolidays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficialHoliday>>> GetOfficialHolidays()
        {
            return await _context.OfficialHolidays.ToListAsync();
        }

        // GET: api/OfficialHolidays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficialHoliday>> GetOfficialHoliday(int id)
        {
            var officialHoliday = await _context.OfficialHolidays.FindAsync(id);

            if (officialHoliday == null)
            {
                return NotFound();
            }

            return officialHoliday;
        }

        // PUT: api/OfficialHolidays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficialHoliday(int id, OfficialHoliday officialHoliday)
        {
            if (id != officialHoliday.Id)
            {
                return BadRequest();
            }

            _context.Entry(officialHoliday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficialHolidayExists(id))
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

        // POST: api/OfficialHolidays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OfficialHoliday>> PostOfficialHoliday(OfficialHoliday officialHoliday)
        {
            _context.OfficialHolidays.Add(officialHoliday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfficialHoliday", new { id = officialHoliday.Id }, officialHoliday);
        }

        // DELETE: api/OfficialHolidays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficialHoliday(int id)
        {
            var officialHoliday = await _context.OfficialHolidays.FindAsync(id);
            if (officialHoliday == null)
            {
                return NotFound();
            }

            _context.OfficialHolidays.Remove(officialHoliday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfficialHolidayExists(int id)
        {
            return _context.OfficialHolidays.Any(e => e.Id == id);
        }
    }
}
