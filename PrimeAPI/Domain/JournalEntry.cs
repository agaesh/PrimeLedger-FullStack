
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JournalEntry
{
    [Key]
    public int Id { get; set; }

    [Column("entry_number")]
    public string EntryNumber { get; set; } // e.g., "JE-2026-001"

    [Column("date", TypeName = "datetime2")]
    public DateTime Date { get; set; }

    public string Description { get; set; }

    // Navigation property: Holds all debit & credit lines for this transaction
    public virtual ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
}