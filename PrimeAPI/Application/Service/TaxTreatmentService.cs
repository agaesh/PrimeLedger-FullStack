using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;
using System.Text.Json;
using PrimeLedger.Shared.DTO.TaxTreatment;
namespace PrimeAPI.Infrastructure.Services
{
    public class TaxTreatmentService : ITaxTreatmentService
    {
        private readonly ITaxTreatmentRepository _repository;

        public TaxTreatmentService(ITaxTreatmentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<TaxTreatment?> GetByIdAsync(int id)
        {
          
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TaxTreatment?> GetByCodeAsync(string code)
        {
            return await _repository.GetByCodeAsync(code);
        }

        public async Task<IEnumerable<TaxTreatment>> GetAllAsync()
        {
            return await _repository.GetAllTaxTreatmentAsync();
        }

        public async Task<IEnumerable<TaxTreatment>> GetByTypeAsync(PrimeLedger.Shared.Enums.TaxCodeType type)
        {
            return await _repository.GetAllTaxTreatmentByTypeAsync(type);
        }

        public async Task<int> AddAsync(TaxTreatmentCreateDTO setupDTO)
        {
            // Business rule: set audit fields

            if (setupDTO.PurchaseGLId == null)
            {
                throw new InvalidOperationException("Purchase GL Account must be provided");
            }

            if (setupDTO.SalesGLId == null)
            {
                throw new InvalidOperationException("Sales GL Account must be provided");
            }

            //Business rule: prevent same GL account for purchase & sales (GST case)
            if (setupDTO.PurchaseGLId == setupDTO.SalesGLId)
            {
                throw new InvalidOperationException("Purchase and Sales accounts cannot be the same for GST.");
            }

            // Map DTO → Domain entity
            var taxCodeSetupDomain = new TaxTreatment
            {
                Code = setupDTO.Code,
                Description = setupDTO.Description,
                TaxRate = setupDTO.TaxRate,
                Type = setupDTO.Type,
                PurchaseGLId = setupDTO.PurchaseGLId,
                SalesGLId = setupDTO.SalesGLId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            return await _repository.AddAsync(taxCodeSetupDomain);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
