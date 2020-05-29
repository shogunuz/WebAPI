using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerDetailController : ControllerBase
    {
        private readonly WorkerDetailContext _context;

        public WorkerDetailController(WorkerDetailContext context)
        {
            _context = context;
        }

        // GET: api/WorkerDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDetail>>> GetWorkerDetails()
        {
            return await _context.WorkerDetails.ToListAsync();
        }

        
        // GET: api/WorkerDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDetail>> GetWorkerDetail(int id)
        {
            var WorkerDetail = await _context.WorkerDetails.FindAsync(id);

            if (WorkerDetail == null)
            {
                return NotFound();
            }
            //WorkerDetail.
            return WorkerDetail;
        }
         


        // PUT: api/WorkerDetail/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkerDetail(int id, WorkerDetail WorkerDetail)
        {
            if (id != WorkerDetail.PMId)
            {
                return BadRequest();
            }

            _context.Entry(WorkerDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerDetailExists(id))
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

        // POST: api/WorkerDetail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkerDetail>> PostWorkerDetail(WorkerDetail workerDetail)
        {
            _context.WorkerDetails.Add(workerDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkerDetail", new { id = workerDetail.PMId }, workerDetail);
        }

        // DELETE: api/WorkerDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkerDetail>> DeleteWorkerDetail(int id)
        {
            var WorkerDetail = await _context.WorkerDetails.FindAsync(id);
            if (WorkerDetail == null)
            {
                return NotFound();
            }

            _context.WorkerDetails.Remove(WorkerDetail);
            await _context.SaveChangesAsync();

            return WorkerDetail;
        }

        private bool WorkerDetailExists(int id)
        {
            return _context.WorkerDetails.Any(e => e.PMId == id);
        }
    }
}
