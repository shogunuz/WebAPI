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

         public WorkerHolidaysController(WorkerHolidayContext context)
        {
            _context = context;
        }

        // GET: api/WorkerHolidays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerHoliday>>> GetWorkerHolidays()
        {
            FreeMem.CollectMethod();
            
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
        [HttpPost]
        public async Task<ActionResult<WorkerHoliday>> PostWorkerHoliday(WorkerHoliday workerHoliday)
        {
            DataRecycle dateRecycle = new DataRecycle();

            if (dateRecycle.HolidayCalc(workerHoliday) == false)
            {
                return BadRequest();
            }
            dateRecycle = null; // обрываем все ссылки на объект, на который ссылался dateRecycle
            FreeMem.CollectMethod();

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
