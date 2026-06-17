using System;
using System.Collections.Generic;
using System.Text;

using PrimeLedger.Shared.Enums;
namespace PrimeLedger.Shared.DTO.GLAccounts
{
    public class GlAccountDTO
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public NormalBalance NormalBalance { get; set; }
        public int? ParentAccountId { get; set; }
        public bool AllowPosting { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
 }
