using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface ITaxCodeSetupRepository
    {
        Task<TaxCodeSetup?> GetByIdAsync(int id);
        Task<TaxCodeSetup?> GetTaxCodeByIDWithHistories(int id);
        Task<TaxCodeSetup?> GetByCodeAsync(string code);

        Task<IEnumerable<TaxCodeSetup>> GetAllTaxCodeAsync();

        Task<IEnumerable<TaxCodeSetup>> GetAllTaxCodeByTypeAsync(TaxCodeType type);

        Task<int> AddAsync(TaxCodeSetup setup);

        Task UpdateAsync(TaxCodeSetup setup);

        Task DeleteAsync(int id);
    }
}
