using PrimeAPI.Domain;
using Microsoft.EntityFrameworkCore;
using PrimeAPI.Infrasfructure;
using PrimeAPI.Application.Interface;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Repositories
{
    public class ProductMetadataRepository : IProductMetadataRepository
    {
        private readonly AppDbContext _context;

        public ProductMetadataRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ✅ Get by Id
        public async Task<ProductMetadata?> GetByIdAsync(int id)
        {
            return await _context.ProductMetadata
                .AsNoTracking() // prevents tracking overhead for read-only queries
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // ✅ Get by Code
        public async Task<List<ProductMetadata>> GetByCodeTypeAsync(Codetype codeType)
        {
            return await _context.ProductMetadata
                .AsNoTracking()
                .Where(x => x.type == codeType)
                .ToListAsync();
        }

        // ✅ Add new entity
        public async Task AddAsync(ProductMetadata entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.ProductMetadata.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // ✅ Update existing entity
        public async Task UpdateAsync(ProductMetadata entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.ProductMetadata.Update(entity);
            await _context.SaveChangesAsync();
        }

        // ✅ Delete by Id
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductMetadata.FindAsync(id);
            if (entity == null) return false;

            // Soft delete: mark as deleted instead of removing
            entity.Status = StatusEnum.INACTIVE; // or use StatusEnum.Deleted if you have an enum
            entity.UpdatedAt = DateTime.UtcNow;

            _context.ProductMetadata.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ Check existence
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ProductMetadata.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ExistChildren(int id)
        {
            return await _context.ProductMetadata
                .Where(x => x.Id == id)
                .AnyAsync(x => x.Children.Any());
        }
    }
}
