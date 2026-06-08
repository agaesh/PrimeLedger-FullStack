using System.ComponentModel.DataAnnotations;
using PrimeLedger.Shared.Enums;

namespace PrimeLedger.Shared.DTO.Products
{
    public class UpdateProductMetadataDTO
    {

        [Required]

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public StatusEnum? Status { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
