using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using Microsoft.EntityFrameworkCore;

namespace PrimeAPI.Application.Service
{
    public class ProductMetadataService
    {
        private readonly AppDbContext _context;

        public ProductMetadataService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductMetadata>> GetByType(Codetype type)
        {
            return await _context.ProductMetadata
                .Where(x => x.type == type)
                .ToListAsync();
        }

        public async Task<ProductMetadata?> GetById(int id, Codetype type)
        {
            return await _context.ProductMetadata
                .FirstOrDefaultAsync(x => x.Id == id && x.type == type);
        }

        public async Task Create(ProductMetadata entity, Codetype type)
        {
            entity.type = type;
            _context.ProductMetadata.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductMetadata entity, Codetype type)
        { 
            entity.type = type;
            _context.ProductMetadata.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, Codetype type)
        {
            var item = await GetById(id, type);
            if (item != null)
            {
                _context.ProductMetadata.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
