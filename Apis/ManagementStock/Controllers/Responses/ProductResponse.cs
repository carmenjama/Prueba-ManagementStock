namespace ManagementProducts.Api.Controllers.Responses
{
    public class ProductResponse
    {
        public long Id { get; set; } = 0;
        public long CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string Unit { get; set; } = string.Empty;
        public decimal Stock { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; } = null;
        public bool HasMultimedia { get; set; } = false;
    }
}
