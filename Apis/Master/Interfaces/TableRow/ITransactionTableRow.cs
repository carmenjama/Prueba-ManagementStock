namespace ManagementProducts.Master.Interfaces.TableRow
{
    public interface ITransactionTableRow : IAuditTableRow
    {
        public long Id { get; set; }
        public long TypeTransactionId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string TypeTransactionName { get; set; }
        public string? Note { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
