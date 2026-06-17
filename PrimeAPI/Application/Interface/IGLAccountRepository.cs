using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.GLAccounts;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface IGLAccountRepository
    {
        Task<IEnumerable<GlAccount>> GetAllAsync();
        Task<GlAccount?> GetByIdAsync(int id);
        Task<GlAccount?> GetByAccountCodeAsync(string accountCode);
        Task<GlAccount> AddAsync(GlAccount account);
        Task UpdateAsync(GlAccount account);
        Task DeleteAsync(int id);

        Task<IEnumerable<GlAccount>> GetByTypeAsync(AccountType type);
    }
}
