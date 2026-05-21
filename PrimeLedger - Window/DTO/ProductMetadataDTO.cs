namespace PrimeLedger_Window.DTO
{
    public class ProductMetadataDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public int? ParentId { get; set; }

        public string createAt { get; set; }

        public string? updateAt { get; set; }
    }
}