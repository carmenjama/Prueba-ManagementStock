namespace ManagementTransaction.Api.Controllers.Payload
{
    public class TransactionPayload
    {
        public long ProductId { get; set; } = 0;
        public string TypeTransactionName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public DateTime TransactionDate { get; set; } = DateTime.MinValue;
    }
}
