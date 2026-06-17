using PrimeLedger.Shared.Enums;

namespace PrimeLedger.Shared.DTO.GLAccounts
{
    public class GlAccountCreateDTO
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public NormalBalance NormalBalance { get; set; }
        public int? ParentAccountId { get; set; }
        public bool AllowPosting { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
