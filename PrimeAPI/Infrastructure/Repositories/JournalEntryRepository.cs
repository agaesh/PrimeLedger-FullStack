using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Interface;
using PrimeAPI.Infrasfructure;

namespace PrimeAPI.Infrastructure.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly AppDbContext _context;

        public JournalEntryRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<JournalEntry?> GetByIdAsync(int id)
        {
            return await _context.JournalEntries
                .Include(entry => entry.Lines)
                .FirstOrDefaultAsync(entry => entry.Id == id);
        }

        public async Task<IEnumerable<JournalEntry>> GetAllAsync()
        {
            return await _context.JournalEntries
                .Include(entry => entry.Lines)
                .ToListAsync();
        }

        public async Task<int> AddAsync(JournalEntry entry)
        {
            _context.JournalEntries.Add(entry);
            await _context.SaveChangesAsync();
            return entry.Id;
        }

        public async Task<bool> UpdateAsync(int id, JournalEntry entry)
        {
            var existingEntry = await _context.JournalEntries
                .Include(existing => existing.Lines)
                .FirstOrDefaultAsync(existing => existing.Id == id);

            if (existingEntry == null)
            {
                return false;
            }

            existingEntry.EntryNumber = entry.EntryNumber;
            existingEntry.Date = entry.Date;
            existingEntry.Description = entry.Description;

            _context.JournalEntryLines.RemoveRange(existingEntry.Lines);
            existingEntry.Lines = entry.Lines
                .Select(line => new JournalEntryLine
                {
                    JournalEntryId = id,
                    AccountId = line.AccountId,
                    Debit = line.Debit,
                    Credit = line.Credit
                })
                .ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.JournalEntries.FindAsync(id);
            if (entry != null)
            {
                _context.JournalEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
}
    }