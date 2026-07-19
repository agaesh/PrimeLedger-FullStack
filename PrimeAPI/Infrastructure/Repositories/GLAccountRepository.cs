using Microsoft.EntityFrameworkCore;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using PrimeLedger.Shared.Enums;
using PrimeLedger.Shared.DTO.GLAccounts;
using PrimeAPI.Application.Interface;

namespace PrimeAPI.Infrastructure.Repositories
{
    public class GLAccountRepository:IGLAccountRepository
    {
        private readonly AppDbContext _context;

        public GLAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all accounts
        public IQueryable<GlAccount> GetAll()
        {
            return _context.GlAccounts
                .Include(a => a.ChildAccounts);
        }

        // ✅ Get account by Id
        public async Task<GlAccount?> GetByIdAsync(int id)
        {
            return await _context.GlAccounts
                .Include(a => a.ChildAccounts)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<GlAccount?> GetByAccountCodeAsync(string accountCode)
        {
            return await _context.GlAccounts
                .Include(a => a.ChildAccounts)
                .FirstOrDefaultAsync(a => a.AccountCode == accountCode);
        }

        // ✅ Add new account
        public async Task<GlAccount> AddAsync(GlAccount account)
        {
            var ent = await _context.GlAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return ent.Entity;
        }

        // ✅ Update existing account
        public async Task UpdateAsync(GlAccount account)
        {
            _context.GlAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        // ✅ Delete account
        public async Task DeleteAsync(int id)
        {
            var account = await _context.GlAccounts.FindAsync(id);
            if (account != null)
            {
                _context.GlAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ Get all accounts by type (e.g., Expense)
        public async Task<IEnumerable<GlAccount>> GetByTypeAsync(AccountType type)
        {
            return await _context.GlAccounts
                .Where(a => a.AccountType == type)
                .ToListAsync();
        }

        public async Task<int> GetTotalRecordsAsync()
        {
            return await _context.GlAccounts.CountAsync();
        }
    }
}
