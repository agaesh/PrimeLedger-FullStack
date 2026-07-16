using Microsoft.AspNetCore.Mvc;
using PrimeAPI.Application.Interface;
using PrimeLedger.Shared.DTO.GLAccounts;
using PrimeLedger.Shared.Enums;
using PrimeAPI.Application.Helpers;

namespace PrimeAPI.Controllers
{
    [Route("PrimeApi/gl-accounts")]
    [ApiController]
    public class GLAccountController : ControllerBase
    {
        private readonly IGLAccountService _service;

        public GLAccountController(IGLAccountService service)
        {
            _service = service;
        }

        // ✅ GET: api/gl-accounts?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<GlAccountDTO>>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var accounts = await _service.GetAllAsync(pageNumber, pageSize);
            var totalRecords = await _service.GetTotalRecordsAsync();
            var pagedResult = new PagedResult<GlAccountDTO>
            {
                Items = accounts,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return Ok(pagedResult);
        }

        // ✅ GET: api/gl-accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GlAccountDTO>> GetById(string id)
        {
            // Prefer account code lookup first (account codes may be numeric strings)
            var byCode = await _service.GetByAccountCodeAsync(id);
            if (byCode != null) return Ok(byCode);

            // Fallback to integer id lookup
            if (int.TryParse(id, out var intId))
            {
                var account = await _service.GetByIdAsync(intId);
                if (account == null) return NotFound();
                return Ok(account);
            }

            return NotFound();
        }

        // ✅ POST: api/gl-accounts
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GlAccountCreateDTO dto)
        {
            var id = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = id }, dto);
        }

        // ✅ PUT: api/gl-accounts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] GlAccountStatusUpdateDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch.");
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        // ✅ DELETE: api/gl-accounts/{idOrCode}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            // Try delete by account code first
            var byCode = await _service.GetByAccountCodeAsync(id);
            if (byCode != null)
            {
                await _service.DeleteAsync(byCode.Id);
                return NoContent();
            }

            // Fallback to numeric id
            if (int.TryParse(id, out var intId))
            {
                await _service.DeleteAsync(intId);
                return NoContent();
            }

            return NotFound();
        }

        // ✅ GET: api/gl-accounts/by-type/Expense?pageNumber=1&pageSize=10
        [HttpGet("by-type/{type}")]
        public async Task<ActionResult<IEnumerable<GlAccountDTO>>> GetByType(AccountType type, int pageNumber = 1, int pageSize = 10)
        {
            var accounts = await _service.GetByTypeAsync(type, pageNumber, pageSize);
            return Ok(accounts);
        }
    }
}
