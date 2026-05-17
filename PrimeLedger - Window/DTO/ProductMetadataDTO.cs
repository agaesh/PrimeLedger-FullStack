namespace PrimeLedger_Window.DTO
{
    internal class ProductMetadataDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public string create_at { get; set; }

        public string update_at { get; set; }
    }
}