using PrimeLedger.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrimeAPI.Domain
{
    public class GlAccount
    {
        public int Id { get; set; }

        [Column("account_code")]
        public string AccountCode { get; set; }

        [Column("account_name")]
        public string AccountName { get; set; }

        [Column("account_type")]
        public AccountType AccountType { get; set; }

        [Column("normal_balance")]
        public NormalBalance NormalBalance { get; set; }

        [Column("parent_acc_id")]
        public Guid? ParentAccountId { get; set; }

        [Column("is_allow_posting")]
        public bool AllowPosting { get; set; } = true;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        // 🔑 Navigation properties
        public GlAccount ParentAccount { get; set; }
        public ICollection<GlAccount> ChildAccounts { get; set; }
    }
}
