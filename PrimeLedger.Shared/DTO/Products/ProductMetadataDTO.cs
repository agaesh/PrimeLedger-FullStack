
using PrimeLedger.Shared.Enums;
namespace PrimeLedger.Shared.DTO.Products
{
    public class ProductMetadataDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Codetype? Type { get; set; }
        public StatusEnum? Status { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}