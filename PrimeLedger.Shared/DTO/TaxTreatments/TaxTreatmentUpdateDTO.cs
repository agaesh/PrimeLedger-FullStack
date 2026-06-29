using System;
using System.ComponentModel.DataAnnotations;
using PrimeLedger.Shared.Enums;

namespace PrimeLedger.Shared.DTO.TaxTreatment
{
    public class TaxTreatmentUpdateDTO
    {
   
        public string Description { get; set; } // Goods and Services Tax

        [Required]
        [Range(0, 100)]
        public decimal TaxRate { get; set; } // 6.00

        // Audit field (set automatically in service layer)
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
