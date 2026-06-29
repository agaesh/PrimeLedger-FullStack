using PrimeLedger.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeAPI.Domain
{
    [Table("TaxTreatment")]
    public class TaxTreatment
    {
        public int Id { get; set; } // PK

        [Column("code")]
        public string Code { get; set; } = string.Empty; // e.g. GST6, SST8

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("tax_percentage")]
        public decimal TaxRate { get; set; } // e.g. 6.00

        [Column("type")]
        public TaxCodeType Type { get; set; } // GST, SST, VAT

        [Column("purchase_gl_id")]
        public int PurchaseGLId { get; set; } // Input tax account
        public GlAccount? PurchaseGL { get; set; }

        [Column("sales_gl_id")]
        public int SalesGLId { get; set; } // Output tax account
        public GlAccount? SalesGL { get; set; }


        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
