
namespace PrimeLedger.Shared.DTO.GLAccounts
{
    public class GlAccountStatusUpdateDTO
    {
        public int Id { get; set; }              // Identify the account
        public bool AllowPosting { get; set; }  // Toggle posting
        public bool IsActive { get; set; }      // Activate/deactivate
        public int? ParentAccountId { get; set; } // Optional re-parenting
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
