using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using PrimeLedger.Shared.Enums;
using System.Threading.Channels;

namespace PrimeAPI.Infrastructure.Repositories
{  
    public class TaxRegimeRepository : ITaxRegimeRepository
    {
        private readonly AppDbContext _context;

        public TaxRegimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaxRegime>> GetAllTaxRegimeAsync(int pageNumber, int pageSize)
        {
            return await _context.TaxRegime
                .OrderBy(h => h.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTaxHistoriesCountAsync()
        {
            return await _context.TaxRegime.CountAsync();
        }

        public async Task<TaxRegime?> GetByIdAsync(int id)
        {
            return  await _context.TaxRegime.FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<IEnumerable<TaxRegime?>> GetByTaxCodeType(TaxCodeType type)
        {
            return _context.TaxRegime
                .Where(h => h.CodeType == type);
        }

        public async Task<TaxRegime> AddAsync(TaxRegime history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));

            await _context.TaxRegime.AddAsync(history);
            await _context.SaveChangesAsync();

            return history; // now has Id populated
        }

        public async Task UpdateAsync(TaxRegime history)
        {
            _context.TaxRegime.Update(history);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TaxRegime.FindAsync(id);
            if (entity != null)
            {
                _context.TaxRegime.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistActiveHistoryByTaxType(TaxCodeType type)
        {
            return await _context.TaxRegime
                .AnyAsync(e => e.CodeType == type && e.IsActive);
        }

        public async Task<TaxRegime?> GetActiveRegime()
        {
            return await _context.TaxRegime.SingleOrDefaultAsync(r => r.IsActive);
        }
    }
}
