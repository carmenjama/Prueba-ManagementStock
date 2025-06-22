using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.Master.Shared;

namespace ManagementProducts.Master.Transaction
{
    public class TransactionTableRow : AuditTableRow, ITransactionTableRow
    {
        public long Id { get; set; } = 0;
        public long TypeTransactionId { get; set; } = 0;
        public long ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public string TypeTransactionName { get; set; } = string.Empty;
        public string? Note { get; set; } = null;
        public decimal Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public DateTime TransactionDate { get; set; } = DateTime.MinValue;
    }
}
