using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Domain;
using PrimeAPI.Infrastructure; // Fixed typo 'f' to 't'
using PrimeAPI.Application.Service;

namespace PrimeAPI.Controllers
{
    [Route("PrimeAPI/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ProductMetadataService _service;

        // Clean Architecture Tip: Only inject the service layer here. Remove AppDbContext!
        public GroupController(ProductMetadataService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup()
        {
            var groups = await _service.GetByType(Codetype.GROUP);
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await _service.GetById(id, Codetype.GROUP);
            if (group == null)
            {
                return NotFound(new { message = $"Product group with ID {id} was not found." });
            }
            return Ok(group);
        }

        [HttpGet("{groupId}/subgroups")]
        public async Task<IActionResult> GetSubGroupsByGroup([FromRoute] int groupId)
        {
            var result = await _service.GetSubByParentCode(Codetype.SUBGROUP, groupId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductGroup(int id, [FromBody] ProductMetadata productgroup)
        {
            if (id != productgroup.Id)
            {
                return BadRequest(new { message = "ID mismatch between route and payload." });
            }

            try
            {
                // Let the service layer handle data validation and db entity state updates
                await _service.Update(productgroup, Codetype.GROUP);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check existence via the service layer, not the context directly
                var exists = await _service.GetById(id, Codetype.GROUP) != null;
                if (!exists)
                {
                    return NotFound(new { message = $"Product group with ID {id} no longer exists." });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the product group.",
                    detail = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProductGroup([FromBody] ProductMetadata productgroup)
        {
            try
            {
                await _service.Create(productgroup, Codetype.GROUP);

                return StatusCode(StatusCodes.Status201Created, new
                {
                    success = true,
                    message = $"Group '{productgroup.Name}' has been created successfully.",
                    data = productgroup
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An unexpected error occurred while creating the product group.",
                    detail = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            try
            {
                await _service.Delete(id, Codetype.GROUP);
                return NoContent(); // 204 NoContent is standard for successful deletions
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An unexpected error occurred while deleting the product group.",
                    detail = ex.Message
                });
            }
        }
    }
}