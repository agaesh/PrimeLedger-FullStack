using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Helpers;
using PrimeAPI.Application.Interface;
using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;
using System.Net.WebSockets;
using System.Reflection.Metadata;

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

       
            var ActiveRegime = await _repository.GetActiveRegime();

            if (ActiveRegime is not null && ActiveRegime.IsActive)
            {
                if (ActiveRegime.EffectiveTo is null && history.IsActive)
                {
                    throw new InvalidOperationException("Please Update Old regim's effective to Date before insert new regime");
                }

                if (history.IsActive)
                {
                    throw new InvalidOperationException("Please Innactive old regime first before insert new active regime");
                }

                bool overlaps =
                   // Case 1: New EffectiveFrom falls inside the active regime
                   (history.EffectiveFrom >= ActiveRegime.EffectiveFrom &&
                    (ActiveRegime.EffectiveTo == null || history.EffectiveFrom <= ActiveRegime.EffectiveTo))

                   ||

                   // Case 2: New EffectiveTo falls inside the active regime
                   (history.EffectiveTo.HasValue &&
                    (ActiveRegime.EffectiveTo == null || history.EffectiveTo.Value >= ActiveRegime.EffectiveFrom) &&
                    history.EffectiveTo.Value <= (ActiveRegime.EffectiveTo ?? DateTime.MaxValue))

                   ||

                   // Case 3: New period fully covers the active regime
                   (history.EffectiveTo.HasValue &&
                    history.EffectiveFrom <= ActiveRegime.EffectiveFrom &&
                    history.EffectiveTo.Value >= (ActiveRegime.EffectiveTo ?? DateTime.MaxValue));

                if (overlaps)
                {
                    throw new InvalidOperationException(
                        "Effective period overlaps with the current active regime record."
                    );
                }
            }

            var entity = new TaxRegime
            {
                CodeType = history.CodeType,
                EffectiveFrom = history.EffectiveFrom,
                EffectiveTo = history.EffectiveTo,
                IsActive = history.IsActive,
            };

            var Createdhistory = await _repository.AddAsync(entity);
           
            return Createdhistory;
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
