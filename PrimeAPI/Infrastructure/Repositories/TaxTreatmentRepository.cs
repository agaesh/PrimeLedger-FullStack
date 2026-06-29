using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using PrimeLedger.Shared.Enums;
namespace PrimeAPI.Infrastructure.Repositories
{
    public class TaxTreatmentRepository : ITaxTreatmentRepository
    {
        private readonly AppDbContext _context;

        public TaxTreatmentRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TaxTreatment?> GetByIdAsync(int id)
        {
            return await _context.TaxTreatments
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<TaxTreatment?> GetByCodeAsync(string code)
        {
            return await _context.TaxTreatments
                .FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<IEnumerable<TaxTreatment>> GetAllTaxTreatmentAsync()
        {
            return await _context.TaxTreatments
                .OrderBy(s => s.Code)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaxTreatment>> GetAllTaxTreatmentByTypeAsync(TaxCodeType type)
        {
            return await _context.TaxTreatments
                .Where(s => s.Type == type || s.Type == TaxCodeType.EXEMPT)
                .ToListAsync();
        }
         
        public async Task<int> AddAsync(TaxTreatment setup)
        {
            _context.TaxTreatments.Add(setup);
            await _context.SaveChangesAsync();

            return setup.Id;
        }

        public async Task UpdateAsync(TaxTreatment setup)
        {
            _context.TaxTreatments.Update(setup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setup = await _context.TaxTreatments.FindAsync(id);
            if (setup != null)
            {
                _context.TaxTreatments.Remove(setup);
                await _context.SaveChangesAsync();
            }
        }
    }
}
