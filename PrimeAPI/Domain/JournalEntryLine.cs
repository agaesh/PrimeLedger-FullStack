using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PrimeAPI.Domain;

public class JournalEntryLine
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("JournalEntry")]
    [Column("journal_entry_id")]
    public int JournalEntryId { get; set; }

    [JsonIgnore]
    public virtual JournalEntry? JournalEntry { get; set; }

    [ForeignKey("Account")]
    [Column("account_id")]
    public int AccountId { get; set; }
    public virtual GlAccount? Account { get; set; }

    [Column("debit", TypeName = "decimal(18, 2)")]
    public decimal Debit { get; set; }

    [Column("credit", TypeName = "decimal(18, 2)")]
    public decimal Credit { get; set; }
}