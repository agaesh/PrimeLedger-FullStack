namespace PrimeAPI.Application.Interface
{
    public interface IJournalEntryRepository
    {
        Task<JournalEntry?> GetByIdAsync(int id);
        Task<IEnumerable<JournalEntry>> GetAllAsync();
        Task<int> AddAsync(JournalEntry entry);
        Task<bool> UpdateAsync(int id, JournalEntry entry);
        Task DeleteAsync(int id);
    }
}
