using Microsoft.AspNetCore.Mvc;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;
using PrimeLedger.Shared.DTO.TaxCodeSetup;
using PrimeAPI.Application.DTOs;

namespace PrimeAPI.Controllers
{
    [ApiController]
    [Route("primeapi/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxCodeSetupService _service;

        public TaxController(ITaxCodeSetupService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/tax/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaxCodeSetup>> GetById(int id)
        {
            var setup = await _service.GetByIdAsync(id);
            if (setup == null)
                return NotFound();

            return Ok(setup);
        }

        // GET: api/tax/code/{code}
        [HttpGet("code/{code}")]
        public async Task<ActionResult<TaxCodeSetup>> GetByCode(string code)
        {
            var setup = await _service.GetByCodeAsync(code);
            if (setup == null)
                return NotFound();

            return Ok(setup);
        }

        // GET: api/tax
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxCodeSetup>>> GetAll()
        {
            var setups = await _service.GetAllAsync();
            return Ok(setups);
        }

        // GET: api/tax/type/{type}
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<TaxCodeSetup>>> GetByType(TaxCodeType type)
        {
            var setups = await _service.GetByTypeAsync(type);
            return Ok(setups);
        }

        // POST: api/tax
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TaxCodeSetupCreateDTO setup)
        {
            if (setup == null)
                return BadRequest("Invalid tax code setup.");

            var createdID = await _service.AddAsync(setup);
            return CreatedAtAction(nameof(GetById), new { id = createdID }, setup);
        }

        // PUT: api/tax/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] TaxCodeSetupUpdateDTO setup)
        {
            if (id == 0)
                return BadRequest("Invalid request.");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateAsync(id,setup);
            return NoContent();
        }

        // DELETE: api/tax/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
