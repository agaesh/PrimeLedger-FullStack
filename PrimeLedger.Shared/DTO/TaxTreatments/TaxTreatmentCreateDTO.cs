
using PrimeLedger.Shared.Enums;

namespace PrimeLedger.Shared.DTO.TaxTreatment
{
    public class TaxTreatmentCreateDTO
    {
        // Identity
        public string Code { get; set; } = string.Empty; // e.g. GST6, SST8
        public string Description { get; set; } = string.Empty;

        // Tax details
        public decimal TaxRate { get; set; } // e.g. 6.00
        public TaxCodeType Type { get; set; } // GST, SST, VAT

        // GL mappings
        public int PurchaseGLId { get; set; } // Input tax account
        public int SalesGLId { get; set; }    // Output tax account
    }
}

