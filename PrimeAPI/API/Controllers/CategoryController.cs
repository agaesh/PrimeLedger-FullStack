using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Service;
using PrimeAPI.Application.Interface;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.API.Controllers
{
    [Route("PrimeApi/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductMetadataService _service;

        public CategoryController(IProductMetadataService service)
        {
            _service = service;
        }

        // GET: PrimeAPI/Category
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.GetByType(Codetype.CATEGORY);
            return Ok(categories);
        }

        // GET: PrimeAPI/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _service.GetById(id, Codetype.CATEGORY);
            if (category == null)
            {
                return NotFound(new { message = $"Category with ID {id} was not found." });
            }
            return Ok(category);
        }

   
        // PUT: PrimeAPI/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] UpdateProductMetadataDTO categoryDTO)
        {
            try
            {
                await _service.UpdateAsync(id, categoryDTO);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _service.GetById(id, Codetype.CATEGORY) != null;
                if (!exists)
                {
                    return NotFound(new { message = $"Category with ID {id} no longer exists." });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.FailureResponse(
                        $"An error occurred while updating the category: {ex.Message}"
                    )
                );
            }
        }

        // POST: PrimeAPI/Category
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CreateProductMetadataDTO category)
        {
            try
            {
                var createdCategory = await _service.CreateAsync(category, Codetype.CATEGORY);
                return StatusCode(
                    StatusCodes.Status201Created,
                    ApiResponse<ProductMetadataDTO>.SuccessResponse(
                        createdCategory,
                        $"Category '{category.Code}' has been created successfully."
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponse<string>.FailureResponse(
                        $"An error occurred while creating the category: {ex.Message}"
                    )
                );
            }
        }

        // DELETE: PrimeAPI/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
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
                        $"An error occurred while deleting the category: {ex.Message}"
                    )
                );
            }
        }
    }
}
