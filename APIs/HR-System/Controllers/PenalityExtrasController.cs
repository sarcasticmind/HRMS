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
    public class PenalityExtrasController : ControllerBase
    {
        private readonly HR_SystemContext _context;

        public PenalityExtrasController(HR_SystemContext context)
        {
            _context = context;
        }

        // GET: api/PenalityExtras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PenalityExtra>>> GetPenalityExtras()
        {
            return await _context.PenalityExtras.ToListAsync();
        }

        //set api/PenalityExtra
        [HttpPost]
        public async Task<ActionResult<WeekEnd>> setPenalityExtra(PenalityExtra PenalityExtra)
        {
            if (PenalityExtraExists())
            {
                await DeletePenalityExtra();
            }
            _context.PenalityExtras.Add(PenalityExtra);
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
        private bool PenalityExtraExists()
        {
            return _context.PenalityExtras.Any();
        }
        // DELETE: api/PenalityExtras/5
        private async Task<IActionResult> DeletePenalityExtra()
        {
            if (!PenalityExtraExists())
            {
                return NotFound();
            }
            else
            {
                var penalityExtra = await _context.PenalityExtras.FirstOrDefaultAsync();

                _context.PenalityExtras.Remove(penalityExtra);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
        #endregion 

        // PUT: api/PenalityExtras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{penalityhour}")]
        //public async Task<IActionResult> PutPenalityExtra(byte penalityhour, PenalityExtra penalityExtra)
        //{
        //    await DeletePenalityExtra(penalityhour);
        //    await PostPenalityExtra(penalityExtra);
        //    return NoContent();
        //}

        // POST: api/PenalityExtras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<PenalityExtra>> PostPenalityExtra(PenalityExtra penalityExtra)
        //{
        //    //can not have more than one row
        //    if (_context.PenalityExtras.FirstOrDefault() != null)
        //        return BadRequest();
        //    _context.PenalityExtras.Add(penalityExtra);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {

        //       throw; 
        //    }

        //    return Ok();
        //}


    }
}
