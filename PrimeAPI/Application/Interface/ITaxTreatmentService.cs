using global::PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.TaxTreatment;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface ITaxTreatmentService
    {
        Task<TaxTreatment?> GetByIdAsync(int id);
        Task<TaxTreatment?> GetByCodeAsync(string code);
        Task<IEnumerable<TaxTreatment>> GetAllAsync();
        Task<IEnumerable<TaxTreatment>> GetByTypeAsync(PrimeLedger.Shared.Enums.TaxCodeType type);
        Task<int> AddAsync(TaxTreatmentCreateDTO setupDTO);
        Task DeleteAsync(int id);
    }
}
