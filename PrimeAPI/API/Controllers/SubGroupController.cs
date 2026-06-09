using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Interface;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.API.Controllers
{
    [Route("PrimeAPI/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private readonly IProductMetadataService _service;

        // Inject only the service layer
        public SubGroupController(IProductMetadataService service)
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
        public async Task<IActionResult> PutSubGroup(int id, [FromBody] UpdateProductMetadataDTO subGroup)
        {

            try
            {
                await _service.UpdateAsync(id,subGroup);

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
                return StatusCode(StatusCodes.Status500InternalServerError,
                ApiResponse<string>.FailureResponse("An error occured while updateing the subgroup:" + ex.Message));
            }
        }

        // POST: PrimeAPI/SubGroup
        [HttpPost]
        public async Task<IActionResult> PostSubGroup([FromBody] CreateProductMetadataDTO subGroup)
        {
            try
            {
                var createdSub = await _service.CreateAsync(subGroup, Codetype.SUBGROUP);

                return StatusCode(StatusCodes.Status201Created,
                ApiResponse<CreateProductMetadataDTO>.SuccessResponse(createdSub,$"Subgroup '{subGroup.Code}' has been created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ApiResponse<string>.FailureResponse("An unexpected error occured while creating the subgroup:" + ex.Message));
            }
        }

        // DELETE: PrimeAPI/SubGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubGroup(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ApiResponse<string>.FailureResponse("An error occured while deleting the subgroup:" + ex.Message));
            }
        }
    }
}