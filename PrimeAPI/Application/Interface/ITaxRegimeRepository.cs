using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface ITaxRegimeRepository
    {
        Task<List<TaxRegime>> GetAllTaxRegimeAsync(int pageNumber, int pageSize);

        Task<int> GetTaxHistoriesCountAsync();

        Task<TaxRegime?> GetByIdAsync(int id);
        Task<IEnumerable<TaxRegime?>> GetByTaxCodeType(TaxCodeType type);

        Task<TaxRegime> AddAsync(TaxRegime history);
        Task UpdateAsync(TaxRegime history);
        Task DeleteAsync(int id);

        Task<bool> ExistActiveHistoryByTaxType(TaxCodeType type);

        Task<TaxRegime?> GetActiveRegime();
    }
}
