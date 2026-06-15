using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using PrimeLedger.Shared.Enums;
namespace PrimeAPI.Infrastructure.Repositories
{
    public class TaxCodeSetupRepository : ITaxCodeSetupRepository
    {
        private readonly AppDbContext _context;

        public TaxCodeSetupRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TaxCodeSetup?> GetByIdAsync(int id)
        {
            return await _context.TaxCodeSetups
                .Include(s => s.Histories)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<TaxCodeSetup?> GetTaxCodeByIDWithHistories(int id)
        {
            return await _context.TaxCodeSetups
                .Include(s => s.Histories)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<TaxCodeSetup?> GetByCodeAsync(string code)
        {
            return await _context.TaxCodeSetups
                .FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<IEnumerable<TaxCodeSetup>> GetAllTaxCodeAsync()
        {
            return await _context.TaxCodeSetups
                .OrderBy(s => s.Code)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaxCodeSetup>> GetAllTaxCodeByTypeAsync(TaxCodeType type)
        {
            return await _context.TaxCodeSetups
                .Where(s => s.Type == type || s.Type == TaxCodeType.EXEMPT)
                .ToListAsync();
        }

        public async Task<int> AddAsync(TaxCodeSetup setup)
        {
            _context.TaxCodeSetups.Add(setup);
            await _context.SaveChangesAsync();

            return setup.Id;
        }

        public async Task UpdateAsync(TaxCodeSetup setup)
        {
            _context.TaxCodeSetups.Update(setup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setup = await _context.TaxCodeSetups.FindAsync(id);
            if (setup != null)
            {
                _context.TaxCodeSetups.Remove(setup);
                await _context.SaveChangesAsync();
            }
        }
    }
}
