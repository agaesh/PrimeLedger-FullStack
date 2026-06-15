using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;
using System.Text.Json;
using PrimeLedger.Shared.DTO.TaxCodeSetup;
using PrimeAPI.Application.DTOs;
namespace PrimeAPI.Infrastructure.Services
{
    public class TaxCodeSetupService : ITaxCodeSetupService
    {
        private readonly ITaxCodeSetupRepository _repository;

        public TaxCodeSetupService(ITaxCodeSetupRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<TaxCodeSetup?> GetByIdAsync(int id)
        {
          
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TaxCodeSetup?> GetByCodeAsync(string code)
        {
            return await _repository.GetByCodeAsync(code);
        }

        public async Task<IEnumerable<TaxCodeSetup>> GetAllAsync()
        {
            return await _repository.GetAllTaxCodeAsync();
        }

        public async Task<IEnumerable<TaxCodeSetup>> GetByTypeAsync(TaxCodeType type)
        {
            return await _repository.GetAllTaxCodeByTypeAsync(type);
        }

        public async Task<int> AddAsync(TaxCodeSetupCreateDTO setupDTO)
        {
            // Business rule: set audit fields

            // Map DTO → Domain entity
            var taxCodeSetupDomain = new TaxCodeSetup
            {
                Code = setupDTO.Code,
                Description = setupDTO.Description,
                TaxRate = setupDTO.TaxRate,
                Type = (TaxCodeType)setupDTO.Type,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Histories = new List<TaxCodeHistory>() // initialize empty
            };

            return await _repository.AddAsync(taxCodeSetupDomain);
        }

        public async Task UpdateAsync(int id, TaxCodeSetupUpdateDTO setup)
        {
            var existing = await _repository.GetByIdAsync(id);
            existing.Code = setup.Code;
            existing.Description = setup.Description;
            existing.TaxRate = setup.TaxRate;
            existing.UpdatedDate = setup.UpdatedDate;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
