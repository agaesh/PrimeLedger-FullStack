using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Interface;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;
namespace PrimeAPI.Controllers
{
    [Route("PrimeAPI/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IProductMetadataService _service;

        // Clean Architecture Tip: Only inject the service layer here. Remove AppDbContext!
        public GroupController(IProductMetadataService service)
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
 
        public async Task<IActionResult> PutProductGroup(int id, UpdateProductMetadataDTO productgroup)
        {
        //    if (id != productgroup.Id)
        //    {
        //        return BadRequest(ApiResponse<string>.FailureResponse("ID mismatch between route and payload."));
        //    }

            try
            {
                var result = await _service.UpdateAsync(id, productgroup);

                return Ok(ApiResponse<UpdateProductMetadataDTO>.SuccessResponse(
                    result,
                    "Updated successfully"
                ));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check existence via the service layer, not the context directly
                var exists = await _service.GetById(id, Codetype.GROUP) != null;
                if (!exists)
                {
                    return NotFound(ApiResponse<string>.FailureResponse(
                        $"Product group with ID {id} no longer exists."
                    ));
                }
                throw; // bubble up if it's a real concurrency conflict
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.FailureResponse(
                        $"An error occurred while updating the product group: {ex.Message}"
                    )
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProductGroup([FromBody] CreateProductMetadataDTO productgroup)
        {
            try
            {
                await _service.CreateAsync(productgroup, Codetype.GROUP);

                return StatusCode(StatusCodes.Status201Created,
                ApiResponse<CreateProductMetadataDTO>.SuccessResponse(productgroup, $"Group '{productgroup.Code}' has been created successfully."));
               
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.FailureResponse(
                        $"An unexpected error occurred while creating the product group: {ex.Message}"
                )); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 NoContent is standard for successful deletions
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.FailureResponse(
                        $"An unexpected error occurred while deleting the product group: {ex.Message}"
                ));
            }
        }
    }
}