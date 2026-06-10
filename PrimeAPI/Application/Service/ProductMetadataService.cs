using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
namespace PrimeAPI.Application.Service
{
    public class ProductMetadataService : IProductMetadataService
    {
 
        private readonly IProductMetadataRepository _repository;

        public ProductMetadataService(IProductMetadataRepository repository)
        {
            _repository= repository;
        }

        public async Task<List<ProductMetadata>> GetByType(Codetype type)
        {
          return await _repository.GetByCodeTypeAsync(type);
        }
        public async Task<List<ProductMetadata>> GetSubByParentCode(Codetype type, int parentId)
        {

            return await _repository.GetByCodeTypeAsync(type)
                .ContinueWith(task => task.Result.Where(x => x.ParentId == parentId).ToList());
        }
        public async Task<ProductMetadata?> GetById(int id, Codetype type)
        {
           return await _repository.GetByIdAsync(id);
        }

        public async Task<CreateProductMetadataDTO> CreateAsync(CreateProductMetadataDTO entity, Codetype type)
        {
            var productMetadata = new ProductMetadata
            {
                Code = entity.Code,
                Description = entity.Description,
                type = type,
                Status = entity.Status,
                ParentId = entity.ParentId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(productMetadata);

            // Map back into DTO for return
            var dto = new CreateProductMetadataDTO
            {
                Code = productMetadata.Code,
                Description = productMetadata.Description,
                Type = (Codetype) productMetadata.type,
                ParentId = productMetadata.ParentId,
                CreatedAt = productMetadata.CreatedAt
            };

            return dto;
        }


        public async Task<UpdateProductMetadataDTO> UpdateAsync(int id, UpdateProductMetadataDTO dto)
        {
      
            var existing = await  GetById(id,Codetype.GROUP);

            existing.Description = dto.Description;
            existing.Status = dto.Status;
            existing.UpdatedAt = dto.UpdatedAt;
            await _repository.UpdateAsync(existing);

            // Return the DTO directly
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            if(id <= 0) throw new ArgumentException("Id must be greater than zero.", nameof(id));

            if (await _repository.ExistChildren(id))
            {
                throw new InvalidOperationException($"Cannot Delete Entity with ID: {id} as the child exists");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
