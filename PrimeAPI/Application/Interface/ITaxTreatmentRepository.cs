using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface ITaxTreatmentRepository
    {
        Task<TaxTreatment?> GetByIdAsync(int id);
        Task<TaxTreatment?> GetByCodeAsync(string code);

        Task<IEnumerable<TaxTreatment>> GetAllTaxTreatmentAsync();

        Task<IEnumerable<TaxTreatment>> GetAllTaxTreatmentByTypeAsync(PrimeLedger.Shared.Enums.TaxCodeType type);

        Task<int> AddAsync(TaxTreatment setup);

        Task DeleteAsync(int id);
    }
}
