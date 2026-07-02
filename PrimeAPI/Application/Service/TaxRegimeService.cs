using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Linq;

namespace PrimeAPI.Application.Services
{
    public class TaxRegimeService : ITaxRegimeService
    {
        private readonly ITaxRegimeRepository _repository;

        public TaxRegimeService(ITaxRegimeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<TaxRegime>> GetAllTaxRegimeAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) throw new ArgumentException("Page number must be greater than 0");
            if (pageSize <= 0) throw new ArgumentException("Page size must be greater than 0");

            var items = await _repository.GetAllTaxRegimeAsync(pageNumber, pageSize);
            var totalRecords = await _repository.GetTaxHistoriesCountAsync();

            return new PagedResult<TaxRegime>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }

        public async Task<TaxRegime?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TaxRegime>> GetByTaxCodeType(TaxCodeType type)
        {
            return await _repository.GetByTaxCodeType(type);
        }

        public async Task<TaxRegime> AddAsync(TaxRegimeCreateDTO history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));


            if (history.IsActive)
            {
                var activeRegime = await _repository.GetActiveRegime();
                if (activeRegime is not null)
                {
                    // Prevent inserting while another regime is active or open-ended
                    if (activeRegime.IsActive || activeRegime.EffectiveTo is null)
                    {
                        throw new InvalidOperationException(
                            "Cannot insert new regime while current regime is active or has no end date."
                        );
                    }
                }
            }

            var entity = new TaxRegime
            {
                CodeType = history.CodeType,
                EffectiveFrom = history.EffectiveFrom,
                EffectiveTo = history.EffectiveTo,
                IsActive = history.IsActive,
            };

            return await _repository.AddAsync(entity);
        }
        public async Task UpdateAsync(TaxRegime history)
        {
            // Same validation rules apply on update
       
            await _repository.UpdateAsync(history);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
