namespace ManagementProducts.SharedDto.Dto
{
    public class ProductDto : AuditDto
    {
        public long? Id { get; set; } = null;
        public long CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string Unit { get; set; } = string.Empty;
        public decimal Stock { get; set; } = 0;
        public byte[]? Data { get; set; } = null;
        public string? Multimedia { get; set; } = null;
        public string? Extension { get; set; } = null;
        public string? Note { get; set; } = null;
        public decimal? FilterPriceMin { get; set; } = null;
        public decimal? FilterPriceMax { get; set; } = null;
        public decimal? FilterStockMin { get; set; } = null;
        public decimal? FilterStockMax { get; set; } = null;
        public bool HasMultimedia { get; set; } = false;
    }
}
