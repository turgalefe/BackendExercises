using Microsoft.AspNetCore.Mvc;
using CrudBackend.Data;
using CrudBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecords()
        {
            return await _context.Records.ToListAsync();
        }

        // GET: api/records/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(int id)
        {
            var record = await _context.Records.FindAsync(id);
            if (record == null) return NotFound();
            return record;
        }

        // POST: api/records
        [HttpPost]
        public async Task<ActionResult<Record>> CreateRecord(Record record)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecord), new { id = record.Id }, record);
        }

        // PUT: api/records/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, Record updatedRecord)
        {
            if (id != updatedRecord.Id)
                return BadRequest(new { error = "Record ID does not match." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var record = await _context.Records.FindAsync(id);
            if (record == null) return NotFound();

            record.Name = updatedRecord.Name;
            record.Email = updatedRecord.Email;
            record.Phone = updatedRecord.Phone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/records/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _context.Records.FindAsync(id);
            if (record == null) return NotFound();

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
