using global::PrimeAPI.Domain;
using PrimeAPI.Application.DTOs;
using PrimeLedger.Shared.DTO.TaxCodeSetup;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface ITaxCodeSetupService
    {
        Task<TaxCodeSetup?> GetByIdAsync(int id);
        Task<TaxCodeSetup?> GetByCodeAsync(string code);
        Task<IEnumerable<TaxCodeSetup>> GetAllAsync();
        Task<IEnumerable<TaxCodeSetup>> GetByTypeAsync(TaxCodeType type);
        Task<int> AddAsync(TaxCodeSetupCreateDTO setupDTO);
        Task UpdateAsync(int id, TaxCodeSetupUpdateDTO setup);
        Task DeleteAsync(int id);
    }
}
