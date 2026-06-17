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
       
        [Column("purchase_account_id")]
        public int? PurchaseAccountId { get; set; }

        [Column("sales_account_id")]
        public int? SalesAccountId { get; set; }

        public GlAccount PurchaseAccount { get; set; }
        public GlAccount SalesAccount  { get; set; }
    }
}
