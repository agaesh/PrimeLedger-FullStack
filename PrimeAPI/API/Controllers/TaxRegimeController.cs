using Microsoft.AspNetCore.Mvc;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.API.Controllers
{
    [ApiController]
    [Route("PrimeApi/tax-regime/")]
    public class TaxRegimeController : ControllerBase
    {
        private readonly ITaxRegimeService _service;

        public TaxRegimeController(ITaxRegimeService service)
        {
            _service = service;
        }

        // 🌍 GLOBAL ENDPOINTS -----------------------------

        // GET: PrimeApi/tax-regim/
        [HttpGet]

        public async Task<ActionResult<PagedResult<TaxRegime>>> GetAllHistoryAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Page number and size must be greater than zero" });

            var result = await _service.GetAllTaxRegimeAsync(pageNumber, pageSize);
            return Ok(result);
        }


        // GET: PrimeApi/tax-regim/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaxRegime>> GetByIdGlobal(int id)
        {
            var history = await _service.GetByIdAsync(id);
            if (history == null)
                return NotFound();

            return Ok(history);
        }

        // 🔹 SCOPED ENDPOINTS -----------------------------

        // GET: PrimeApi/tax-regim/GST
        [HttpGet("{CodeType}/")]
        public async Task<ActionResult<IEnumerable<TaxRegime>>> GetBySetupId(TaxCodeType CodeType)
        {
            var histories = await _service.GetByTaxCodeType(CodeType);
            return Ok(histories);
        }

        // GET: PrimeApi/tax-regim/CodeType/{id}
        [HttpGet("{CodeType}/{id}")]
        public async Task<ActionResult<TaxRegime>> GetByIdScoped(TaxCodeType type, int id)
        {
            var history = await _service.GetByIdAsync(id);
            if (history == null || history.CodeType != type)
                return NotFound();

            return Ok(history);
        }

        // POST: PrimeApi/tax-regim/
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TaxRegimeCreateDTO regime)
        {
            var createdHistory = await _service.AddAsync(regime);
            return CreatedAtAction(
                nameof(GetByIdScoped),
                new { codeType = createdHistory.CodeType, id = createdHistory.Id },
                createdHistory
            );
        }

        // PUT: PrimeApi/tax-regim/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TaxRegime regime)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid ID" });

            await _service.UpdateAsync(regime);
            return NoContent();
        }

        // DELETE: PrimeApi/tax-regim/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException("Invalid Tax History Id has been provided");
            }
            var history = await _service.GetByIdAsync(id);
            if (history == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
