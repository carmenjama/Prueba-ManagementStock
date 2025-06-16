using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.Master.Shared;

namespace ManagementProducts.Master.TypeTransaction
{
    public class TypeTransactionTableRow : AuditTableRow, ITypeTransactionTableRow
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}