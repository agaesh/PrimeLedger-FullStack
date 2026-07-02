using PrimeAPI.Application.Helpers;
using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface ITaxRegimeService
    {
        Task<PagedResult<TaxRegime>> GetAllTaxRegimeAsync(int pageNumber, int pageSize);

        Task<TaxRegime?> GetByIdAsync(int id);
        Task<IEnumerable<TaxRegime>> GetByTaxCodeType(TaxCodeType type);
        Task<TaxRegime> AddAsync(TaxRegimeCreateDTO history);
        Task UpdateAsync(TaxRegime history);
        Task DeleteAsync(int id);
    }
}
