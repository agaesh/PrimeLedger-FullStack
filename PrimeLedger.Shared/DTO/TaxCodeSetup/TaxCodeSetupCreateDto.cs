using PrimeLedger.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeLedger.Shared.DTO.TaxCodeSetup
{
    public class TaxCodeSetupCreateDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal TaxRate { get; set; }
        public TaxCodeType Type { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
