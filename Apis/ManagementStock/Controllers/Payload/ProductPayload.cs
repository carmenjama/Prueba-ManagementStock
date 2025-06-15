using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers.Payload
{
    public class ProductPayload
    {
        [Required]
        public long CategoryId { get; set; } = 0;
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; } = 0;
        [Required]
        public string Unit { get; set; } = string.Empty;
        public string? Multimedia { get; set; } = null;
        public string? Note { get; set; } = null;
    }
}
