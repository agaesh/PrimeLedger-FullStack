using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Service;
using PrimeAPI.Domain;

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
        public async Task<IActionResult> PutBrand(int id, [FromBody] ProductMetadata brand)
        {
            if (id != brand.Id)
            {
                return BadRequest(new { message = "ID mismatch between route and payload." });
            }

            try
            {
                // Service layer handles validation + EF state tracking
                await _service.Update(brand, Codetype.BRAND);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the brand.",
                    detail = ex.Message
                });
            }
        }

        // POST: PrimeAPI/Brand
        [HttpPost]
        public async Task<IActionResult> PostBrand([FromBody] ProductMetadata brand)
        {
            try
            {
                await _service.Create(brand, Codetype.BRAND);

                return StatusCode(StatusCodes.Status201Created, new
                {
                    success = true,
                    message = $"Brand '{brand.Name}' has been created successfully.",
                    data = brand
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An unexpected error occurred while creating the brand.",
                    detail = ex.Message
                });
            }
        }

        // DELETE: PrimeAPI/Brand/5
        [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                await _service.Delete(id, Codetype.BRAND);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An unexpected error occurred while deleting the brand.",
                    detail = ex.Message
                });
            }
        }
    }
}
