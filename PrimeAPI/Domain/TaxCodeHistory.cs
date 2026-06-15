using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrimeAPI.Domain
{
    [Table("TaxCodeHistory")]
    public class TaxCodeHistory
    {
        public int Id { get; set; } // PK
        [Column("tax_code_setup_id")]
        public int TaxCodeSetupId { get; set; } // FK to Setup
        [Column("effective_from")]
        public DateTime EffectiveFrom { get; set; }
        [Column("effective_to")]
        public DateTime? EffectiveTo { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("tax_direction")]
        public string TaxDirection { get; set; } // Sales or Purchase
        [Column("tax_account_id")]
        public int? TaxAccountId { get; set; } // FK to Chart of Accounts
    }
}
