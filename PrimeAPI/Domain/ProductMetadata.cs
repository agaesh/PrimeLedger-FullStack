using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeLedger.Shared.Enums;
namespace PrimeAPI.Domain
{


    public class ProductMetadata
    {
        public int Id { get; private set; }

        [Required]
        public string Code { get; set; }

        //[Required]
        //public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public Codetype? type { get; set; }

        [Required]
        [Column("status")]
        public StatusEnum? Status { get; set; }

        // Scalar FK column
        [Column("parent_id")]
        public int? ParentId { get; set; }
        public ProductMetadata? Parent { get; set; }   // navigation property

        public ICollection<ProductMetadata> Children { get; set; } = new List<ProductMetadata>();

        public bool? isDeleted { get; set;} 

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
