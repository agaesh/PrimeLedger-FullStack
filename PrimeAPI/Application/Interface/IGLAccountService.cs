using PrimeLedger.Shared.DTO.GLAccounts;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface IGLAccountService
    {
        Task<IEnumerable<GlAccountDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<GlAccountDTO> GetByIdAsync(int id);
        Task<GlAccountDTO> GetByAccountCodeAsync(string accountCode);
        Task<int> AddAsync(GlAccountCreateDTO dto);
        Task UpdateAsync(GlAccountStatusUpdateDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GlAccountDTO>> GetByTypeAsync(AccountType type, int pageNumber = 1, int pageSize = 10);
    }
}
