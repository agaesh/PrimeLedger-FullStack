using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Domain
{
    [Table("TaxCodeSetup")]
    public class TaxCodeSetup
    {
        public int Id { get; set; } // PK
        [Column("code")]
        public string Code { get; set; } // GST6
        [Column("description")]
        public string Description { get; set; } // Goods and Services Tax
        [Column("tax_rate")]
        public decimal TaxRate { get; set; } // 6.00

        [Column("tax_type")]
        public TaxCodeType type { get; set; }

        // Audit fields
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
        public ICollection<TaxCodeHistory> Histories { get; set; }
    }
}
