using PrimeLedger.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeLedger.Shared.DTO.TaxRegime
{
    public class TaxRegimeCreateDTO
    {

        public TaxCodeType CodeType { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }
    }

}
