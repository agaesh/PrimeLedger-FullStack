
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface IProductMetadataRepository
    {
        Task<ProductMetadata?> GetByIdAsync(int id);
        Task<List<ProductMetadata>> GetByCodeTypeAsync(Codetype codeType);
        Task AddAsync(ProductMetadata entity);

        Task UpdateAsync(ProductMetadata entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistChildren(int id);
    }
}
