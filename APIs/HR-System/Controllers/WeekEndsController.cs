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
    public class WeekEndsController : ControllerBase
    {
        private readonly HR_SystemContext _context;

        public WeekEndsController(HR_SystemContext context)
        {
            _context = context;
        }

        // GET: api/WeekEnds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeekEnd>>> GetWeekEnds()
        {
            return await _context.WeekEnds.ToListAsync();
        }

        //set api/WeekEnds

        [HttpPost]
        public async Task<ActionResult<WeekEnd>> PostWeekEnd(WeekEnd weekEnd)
        {
            if (WeekEndExists())
            {
                await DeleteWeekEnd();
            }
            _context.WeekEnds.Add(weekEnd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }

        #region private functions

        private bool WeekEndExists()
        {
            return _context.WeekEnds.Any();
        }

        private async Task<IActionResult> DeleteWeekEnd()
        {
            var weekEnd = await _context.WeekEnds.FirstOrDefaultAsync();
            if (!WeekEndExists())
            {
                return NotFound();
            }

            _context.WeekEnds.Remove(weekEnd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        // PUT: api/WeekEnds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("weekend1")]
        //public async Task<IActionResult> PutWeekEnd(string weekend1, WeekEnd weekEnd)
        //{
        //    await DeleteWeekEnd();
        //    await PostWeekEnd(weekEnd);
        //    return NoContent();
        //}

        // POST: api/WeekEnds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<WeekEnd>> PostWeekEnd(WeekEnd weekEnd)
        //{
        //    if (WeekEndExists())
        //        return BadRequest();
        //    _context.WeekEnds.Add(weekEnd);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        throw;
        //    }

        //    return Ok();
        //}

    }
}
