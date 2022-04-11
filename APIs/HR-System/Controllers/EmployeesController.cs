using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_System.Models;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly HR_SystemContext _context;

        public EmployeesController(HR_SystemContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/Employees/Ali
        [HttpGet("{name:alpha}")]
        public async Task<ActionResult<Employee>> GetEmployee(string name)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Name == name);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id || !validEmp(employee))
            {
                return BadRequest();
            }

            if (employee.ExtraHour == 0)
                employee.ExtraHour = _context.PenalityExtras.FirstOrDefault().Extrahour;
            if (employee.PenaltyHour == 0)
                employee.PenaltyHour = _context.PenalityExtras.FirstOrDefault().Penalityhour;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (!validEmp(employee) || !ValidNationalIdAndName(employee))
                return BadRequest();
            if (employee.ExtraHour == 0)
                employee.ExtraHour = _context.PenalityExtras.FirstOrDefault().Extrahour;
            if (employee.PenaltyHour == 0)
                employee.PenaltyHour = _context.PenalityExtras.FirstOrDefault().Penalityhour;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        #region validations
        private bool validEmp(Employee emp)
        {
            return validDateOfContract(emp)
                   && ValidAttendLeaveTime(emp);
        }
        private bool validDateOfContract(Employee emp)
        {
            return emp.DateOfContract >= emp.DateOfBirth.AddYears(20)
                    && emp.DateOfContract > new DateTime(2008, 1, 1);
        }
        private bool ValidAttendLeaveTime(Employee emp)
        {
            return emp.LeavingTime > emp.AttendingTime;
        }
        private bool ValidNationalIdAndName(Employee emp)
        {
            Employee empInDataBase = _context.Employees.FirstOrDefault
                    (e => emp.NationalId == e.NationalId || emp.Name == e.Name);
            return (empInDataBase == null) ? true : false;
        }
        #endregion
    }
}
