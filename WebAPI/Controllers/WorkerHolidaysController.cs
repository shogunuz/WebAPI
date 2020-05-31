using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.somefeatures;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerHolidaysController : ControllerBase
    {
        private readonly WorkerHolidayContext _context;
        private string VarForDate { get; set; }

        private DateRecycle dateRecycle = new DateRecycle();
        

        public WorkerHolidaysController(WorkerHolidayContext context)
        {
            _context = context;
        }

        // GET: api/WorkerHolidays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerHoliday>>> GetWorkerHolidays()
        {
            return await _context.WorkerHolidays.ToListAsync();
        }

        // GET: api/WorkerHolidays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerHoliday>> GetWorkerHoliday(int id)
        {
            var workerHoliday = await _context.WorkerHolidays.FindAsync(id);

            if (workerHoliday == null)
            {
                return NotFound();
            }

            return workerHoliday;
        }

        // PUT: api/WorkerHolidays/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerHoliday(int id, WorkerHoliday workerHoliday)
        {
            if (id != workerHoliday.PMId)
            {
                return BadRequest();
            }

            _context.Entry(workerHoliday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerHolidayExists(id))
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

        // POST: api/WorkerHolidays
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkerHoliday>> PostWorkerHoliday(WorkerHoliday workerHoliday)
        {
          
            //VarForDate = dateRecycle.RecyclingDate((workerHoliday.Date).ToString());
           
            workerHoliday.Date = dateRecycle.RecyclingDate((workerHoliday.Date).ToString());

            _context.WorkerHolidays.Add(workerHoliday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerHoliday", new { id = workerHoliday.PMId }, workerHoliday);
        }

        // DELETE: api/WorkerHolidays/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkerHoliday>> DeleteWorkerHoliday(int id)
        {
            var workerHoliday = await _context.WorkerHolidays.FindAsync(id);
            if (workerHoliday == null)
            {
                return NotFound();
            }

            _context.WorkerHolidays.Remove(workerHoliday);
            await _context.SaveChangesAsync();

            return workerHoliday;
        }

        private bool WorkerHolidayExists(int id)
        {
            return _context.WorkerHolidays.Any(e => e.PMId == id);
        }

    }
}
