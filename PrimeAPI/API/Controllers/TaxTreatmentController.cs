using Microsoft.AspNetCore.Mvc;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;
using PrimeLedger.Shared.DTO.TaxTreatment;
using PrimeLedger.Shared.DTO;

namespace PrimeAPI.Controllers
{
    [ApiController]
    [Route("PrimeApi/tax-treatment")]
    public class TaxTreatmentController : ControllerBase
    {
        private readonly ITaxTreatmentService _service;

        public TaxTreatmentController(ITaxTreatmentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/tax/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaxTreatment>> GetById(int id)
        {
            var setup = await _service.GetByIdAsync(id);
            if (setup == null)
                return NotFound();

            return Ok(setup);
        }

        // GET: api/tax/code/{code}
        [HttpGet("code/{code}")]
        public async Task<ActionResult<TaxTreatment>> GetByCode(string code)
        {
            var setup = await _service.GetByCodeAsync(code);
            if (setup == null)
                return NotFound();

            return Ok(setup);
        }

        // GET: api/tax
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxTreatment>>> GetAll()
        {
            var setups = await _service.GetAllAsync();
            return Ok(setups);
        }

        // GET: api/tax/type/{type}
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<TaxTreatment>>> GetByType(PrimeLedger.Shared.Enums.TaxCodeType type)
        {
            var setups = await _service.GetByTypeAsync(type);
            return Ok(setups);
        }

        // POST: primeapi/tax
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TaxTreatmentCreateDTO>>> Create([FromBody] TaxTreatmentCreateDTO setup)
        {
            if (setup == null)
                return BadRequest(ApiResponse<TaxTreatmentCreateDTO>.Fail("Invalid tax code setup."));

            try
            {
                var createdID = await _service.AddAsync(setup);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdID },
                    ApiResponse<TaxTreatmentCreateDTO>.Ok(setup, "Tax treatment created successfully.")
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<TaxTreatmentCreateDTO>.Fail(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<TaxTreatmentCreateDTO>.Fail("An unexpected error occurred.", ex.Message));
            }
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
