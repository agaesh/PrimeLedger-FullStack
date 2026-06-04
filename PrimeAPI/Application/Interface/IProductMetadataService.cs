using PrimeAPI.Application.Helpers;
using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Interface
{
    public interface IProductMetadataService
    {
        Task<List<ProductMetadata>> GetByType(Codetype type);
        Task<List<ProductMetadata>> GetSubByParentCode(Codetype type, int parentId);

        Task<ProductMetadata?> GetById(int id, Codetype type);

        Task<CreateProductMetadataDTO> CreateAsync(CreateProductMetadataDTO entity, Codetype type);

        Task<UpdateProductMetadataDTO> UpdateAsync(int id, UpdateProductMetadataDTO dto);

        Task DeleteAsync(int id);
    }
}
