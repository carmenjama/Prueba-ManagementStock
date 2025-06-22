namespace ManagementProducts.SharedDto.Dto
{
    public class TransactionDto : AuditDto
    {
        public long Id { get; set; } = 0;
        public long TypeTransactionId { get; set; } = 0;
        public long ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public string? Note { get; set; } = null;
        public string TypeTransactionName { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public DateTime TransactionDate { get; set; } = DateTime.MinValue;
        public DateTime? FilterMinDate { get; set; } = null;
        public DateTime? FilterMaxDate { get; set; } = null;
    }
}
