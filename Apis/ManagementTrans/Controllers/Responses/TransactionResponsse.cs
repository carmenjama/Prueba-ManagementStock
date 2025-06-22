namespace ManagementTrans.Api.Controllers.Responses
{
    public class TransactionResponsse
    {
        public long Id { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public string TypeTransactionName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public DateTime TransactionDate { get; set; } = DateTime.MinValue;
    }
}
