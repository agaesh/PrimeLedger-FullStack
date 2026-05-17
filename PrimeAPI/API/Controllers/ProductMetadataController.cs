using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;

[Route("PrimeAPI/[controller]")]
[ApiController]
public class ProductMetadataController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductMetadataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<ProductMetadata>>> GetByType(Codetype type)
    {
        return await _context.ProductMetadata
            .Where(x => x.type == type)
            .ToListAsync();
    }
    [HttpGet("{type}/{id}")]
    public async Task<ActionResult<ProductMetadata>> GetById(Codetype type ,int id)
    {
        var product = await _context.ProductMetadata
           .FirstOrDefaultAsync(x => x.type == type && x.Id == id);

        if (product == null)
            return NotFound(); // prevents null exception

        return product;

    }

    // PUT: api/ProductMetadata/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductMetadata(int? id, ProductMetadata productmetadata)
    {
        if (id != productmetadata.Id)
        {
            return BadRequest();
        }

        _context.Entry(productmetadata).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductMetadataExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/ProductMetadata
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductMetadata>> PostProductMetadata(ProductMetadata productmetadata)
    {
        _context.ProductMetadata.Add(productmetadata);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProductMetadata", new { id = productmetadata.Id }, productmetadata);
    }

    // DELETE: api/ProductMetadata/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductMetadata(int? id)
    {
        var productmetadata = await _context.ProductMetadata.FindAsync(id);
        if (productmetadata == null)
        {
            return NotFound();
        }

        _context.ProductMetadata.Remove(productmetadata);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductMetadataExists(int? id)
    {
        return _context.ProductMetadata.Any(e => e.Id == id);
    }
}
