using PrimeLedger.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace PrimeLedger.Shared.DTO.Products
{
    public class CreateProductMetadataDTO
    {
        private int id { get; set; }

        public int Id => id; // read-only public accessor

        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public Codetype Type { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public StatusEnum Status { get; set; }

        public int? ParentId { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
