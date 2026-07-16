    using global::PrimeAPI.Application.Interface;
    using global::PrimeAPI.Domain;
    using PrimeLedger.Shared.DTO.GLAccounts;
    using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.Services
{
    public class GLAccountService : IGLAccountService
    {
        private readonly IGLAccountRepository _repository;



        public GLAccountService(IGLAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<GlAccountDTO> GetByAccountCodeAsync(string accountCode)
        {
            var account = await _repository.GetByAccountCodeAsync(accountCode);
            if (account == null) return null;

            return new GlAccountDTO
            {
                Id = account.Id,
                AccountCode = account.AccountCode,
                AccountName = account.AccountName,
                AccountType = account.AccountType,
                NormalBalance = account.NormalBalance,
                ParentAccountId = account.ParentAccountId,
                AllowPosting = account.AllowPosting,
                IsActive = account.IsActive,
                CreatedDate = account.CreatedDate,
                UpdatedDate = account.UpdatedDate
            };
        }

        // ✅ Paginated GetAll
        public async Task<IEnumerable<GlAccountDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var accounts = await _repository.GetAllAsync();
            return accounts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new GlAccountDTO
                {
                    Id = a.Id,
                    AccountCode = a.AccountCode,
                    AccountName = a.AccountName,
                    AccountType = a.AccountType,
                    NormalBalance = a.NormalBalance,
                    ParentAccountId = a.ParentAccountId,
                    AllowPosting = a.AllowPosting,
                    IsActive = a.IsActive,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate
                });
        }

        public async Task<GlAccountDTO> GetByIdAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return null;

            return new GlAccountDTO
            {
                Id = account.Id,
                AccountCode = account.AccountCode,
                AccountName = account.AccountName,
                AccountType = account.AccountType,
                NormalBalance = account.NormalBalance,
                ParentAccountId = account.ParentAccountId,
                AllowPosting = account.AllowPosting,
                IsActive = account.IsActive,
                CreatedDate = account.CreatedDate,
                UpdatedDate = account.UpdatedDate
            };
        }

        public async Task<int> AddAsync(GlAccountCreateDTO dto)
        {
            var account = new GlAccount
            {
                AccountCode = dto.AccountCode,
                AccountName = dto.AccountName,
                AccountType = dto.AccountType,
                NormalBalance = dto.NormalBalance,
                ParentAccountId = dto.ParentAccountId,
                AllowPosting = dto.AllowPosting,
                IsActive = dto.IsActive,
                CreatedDate = dto.CreatedDate
            };
            var created = await _repository.AddAsync(account);
            return created.Id;
        }

        public async Task UpdateAsync(GlAccountStatusUpdateDTO dto)
        {
            var account = await _repository.GetByIdAsync(dto.Id);
            if (account == null) return;

            // Only update allowed fields
            account.AllowPosting = dto.AllowPosting;
            account.IsActive = dto.IsActive;
            account.ParentAccountId = dto.ParentAccountId;
            account.UpdatedDate = dto.UpdatedDate;

            await _repository.UpdateAsync(account);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GlAccountDTO>> GetByTypeAsync(AccountType type, int pageNumber = 1, int pageSize = 10)
        {
            var accounts = await _repository.GetByTypeAsync(type);
            return accounts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new GlAccountDTO
                {
                    Id = a.Id,
                    AccountCode = a.AccountCode,
                    AccountName = a.AccountName,
                    AccountType = a.AccountType,
                    NormalBalance = a.NormalBalance,
                    ParentAccountId = a.ParentAccountId,
                    AllowPosting = a.AllowPosting,
                    IsActive = a.IsActive,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate
                });
        }

        public Task<int> GetTotalRecordsAsync()
        {
            return _repository.GetTotalRecordsAsync();
        }
    }
}