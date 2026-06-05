using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Service;
using PrimeLedger.Shared.DTO.Products; // Fixed typo 'f' to 't
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.API.Controllers
{
    [Route("PrimeApi/[Controller]")]
    [ApiController]
    public class BrandController:ControllerBase
    {
        private readonly ProductMetadataService _service;

        public BrandController(ProductMetadataService service)
        {
            _service = service;
        }
        public async Task<IActionResult> GetBrands()
        {
           var brands = await _service.GetByType(Codetype.BRAND);
           return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _service.GetById(id, Codetype.GROUP);
            if (brand == null)
            {
                return NotFound(new { message = $"Product group with ID {id} was not found." });
            }
            return Ok(brand);
        }

        [HttpGet("{BrandId}/categories")]
        public async Task<IActionResult> GetCategoryByBrand(int BrandId) {
            var category = await _service.GetSubByParentCode(Codetype.SUBGROUP, BrandId);
            return Ok(category);
        
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, [FromBody] UpdateProductMetadataDTO brandDTO)
        {
            try
            {
                // Service layer handles validation + EF state tracking
                await _service.UpdateAsync(id,brandDTO);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check existence via service layer
                var exists = await _service.GetById(id, Codetype.BRAND) != null;
                if (!exists)
                {
                    return NotFound(new { message = $"Brand with ID {id} no longer exists." });
                }
                throw;
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

        // POST: PrimeAPI/Brand
        [HttpPost]
        public async Task<IActionResult> PostBrand([FromBody] CreateProductMetadataDTO brand)
        {
            try
            {
                var CreateBrand = await _service.CreateAsync(brand, Codetype.BRAND);
                return StatusCode(
                    StatusCodes.Status201Created,
                    ApiResponse<CreateProductMetadataDTO>.SuccessResponse(
                        CreateBrand,
                        $"Brand '{brand.Code}' has been created successfully."
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 ApiResponse<string>.FailureResponse(
                     $"An error occurred while creating the product group: {ex.Message}"
                 )
              );
            }
        }

        // DELETE: PrimeAPI/Brand/5
        [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ApiResponse<string>.FailureResponse(
                    $"An error occurred while deleting the product group: {ex.Message}"
                ));
            }
        }
    }
}
