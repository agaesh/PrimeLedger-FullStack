using PrimeLedger.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeLedger.Shared.DTO.GLAccounts
{
    public class GLAccountFilterDTO
    {
        public string? search { get; set; }
        public AccountType? type { get; set; }
        public bool isActive { get; set; } = true;
    }
}
