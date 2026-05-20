using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Service;
using PrimeAPI.Domain;

namespace PrimeAPI.API.Controllers
{
    [Route("PrimeAPI/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private readonly ProductMetadataService _service;

        // Inject only the service layer
        public SubGroupController(ProductMetadataService service)
        {
            _service = service;
        }

        // GET: PrimeAPI/SubGroup
        [HttpGet]
        public async Task<IActionResult> GetSubGroups()
        {
            var subGroups = await _service.GetByType(Codetype.SUBGROUP);
            return Ok(subGroups);
        }

        // GET: PrimeAPI/SubGroup/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubGroup(int id)
        {
            var subGroup = await _service.GetById(id, Codetype.SUBGROUP);

            if (subGroup == null)
            {
                return NotFound(new
                {
                    message = $"SubGroup with ID {id} was not found."
                });
            }

            return Ok(subGroup);
        }

        // PUT: PrimeAPI/SubGroup/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubGroup(int id, [FromBody] ProductMetadata subGroup)
        {
            if (id != subGroup.Id)
            {
                return BadRequest(new
                {
                    message = "ID mismatch between route and payload."
                });
            }

            try
            {
                await _service.Update(subGroup, Codetype.SUBGROUP);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _service.GetById(id, Codetype.SUBGROUP) != null;

                if (!exists)
                {
                    return NotFound(new
                    {
                        message = $"SubGroup with ID {id} no longer exists."
                    });
                }

                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the subgroup.",
                    detail = ex.Message
                });
            }
        }

        // POST: PrimeAPI/SubGroup
        [HttpPost]
        public async Task<IActionResult> PostSubGroup([FromBody] ProductMetadata subGroup)
        {
            try
            {
                await _service.Create(subGroup, Codetype.SUBGROUP);

                return StatusCode(StatusCodes.Status201Created, new
                {
                    success = true,
                    message = $"SubGroup '{subGroup.Name}' has been created successfully.",
                    data = subGroup
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An unexpected error occurred while creating the subgroup.",
                    detail = ex.Message
                });
            }
        }

        // DELETE: PrimeAPI/SubGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubGroup(int id)
        {
            try
            {
                await _service.Delete(id, Codetype.SUBGROUP);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An unexpected error occurred while deleting the subgroup.",
                    detail = ex.Message
                });
            }
        }
    }
}