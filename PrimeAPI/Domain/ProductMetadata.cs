using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace PrimeAPI.Domain
{

    public enum Codetype { 
        GROUP,
        BRAND,
        UOM
    }

    public enum StatusEnum
    {
        ACTIVE,
        INACTIVE
    }

    public class ProductMetadata
    {
        public int Id { get; private set; }

        [Required]
        public string Code { get; private set; }

        [Required]
        public string Name { get; set; }

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


        [Column("created_at")]
        public DateTime createdAt { get; set; }

        [Column("updated_at")]
        public DateTime? updatedAt { get; set; }
    }
}
