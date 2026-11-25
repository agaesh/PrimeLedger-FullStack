using PrimeAPI.Application.Interface;

namespace PrimeAPI.Application.Service
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _repository;

        public JournalEntryService(IJournalEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<JournalEntry?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<JournalEntry>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<int> AddAsync(JournalEntry entry)
        {
            ValidateEntry(entry);
            return _repository.AddAsync(entry);
        }

        public Task<bool> UpdateAsync(int id, JournalEntry entry)
        {
            ValidateEntry(entry);
            return _repository.UpdateAsync(id, entry);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        private static void ValidateEntry(JournalEntry entry)
        {
            ArgumentNullException.ThrowIfNull(entry);

            if (entry.Lines == null || entry.Lines.Count < 2)
            {
                throw new ArgumentException("A journal voucher must contain at least two lines.", nameof(entry));
            }

            if (entry.Lines.Any(line =>
                    line.Debit < 0 ||
                    line.Credit < 0 ||
                    (line.Debit > 0) == (line.Credit > 0)))
            {
                throw new ArgumentException(
                    "Each journal line must have either a positive debit or a positive credit, but not both.",
                    nameof(entry));
            }

            var totalDebits = entry.Lines.Sum(line => line.Debit);
            var totalCredits = entry.Lines.Sum(line => line.Credit);

            if (totalDebits != totalCredits)
            {
                throw new ArgumentException("Total debits must equal total credits.", nameof(entry));
            }
        }
    }
}
