using Microsoft.AspNetCore.Mvc;
using PrimeAPI.Application.Interface;

namespace PrimeAPI.Controllers
{
    [ApiController]
    [Route("PrimeApi/journal-vouchers")]
    public class JournalVoucherController : ControllerBase
    {
        private readonly IJournalEntryService _service;

        public JournalVoucherController(IJournalEntryService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalEntry>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<JournalEntry>> GetById(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            return entry == null ? NotFound() : Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<JournalEntry>> Create([FromBody] JournalEntry entry)
        {
            try
            {
                var id = await _service.AddAsync(entry);
                entry.Id = id;
                return CreatedAtAction(nameof(GetById), new { id }, entry);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] JournalEntry entry)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, entry);
                return updated ? NoContent() : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
