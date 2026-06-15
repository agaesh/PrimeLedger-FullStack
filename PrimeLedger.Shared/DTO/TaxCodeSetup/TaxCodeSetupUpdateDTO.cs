using System;
using System.ComponentModel.DataAnnotations;
using PrimeLedger.Shared.Enums;

namespace PrimeAPI.Application.DTOs
{
    public class TaxCodeSetupUpdateDTO
    {
        [Required]
        [MaxLength(20)]
        public string Code { get; set; } // GST6

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } // Goods and Services Tax

        [Required]
        [Range(0, 100)]
        public decimal TaxRate { get; set; } // 6.00

        [Required]
        public TaxCodeType Type { get; set; }

        // Audit field (set automatically in service layer)
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
