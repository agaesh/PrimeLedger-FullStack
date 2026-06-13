
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;
using PrimeLedger.Shared.DTO.Products;

namespace PrimeAPI.Application.Interface
{
    public interface IProductMetadataRepository
    {
        Task<ProductMetadata?> GetByIdAsync(int id);
        Task<List<ProductMetadata>> GetByCodeTypeAsync(Codetype codeType);
        Task<ProductMetadata> AddAsync(ProductMetadata entity);

        Task UpdateAsync(ProductMetadata entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistChildren(int id);
    }
}
