using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.Master.Shared;

namespace ManagementProducts.Master.TypeTransaction
{
    public partial class TransactionRepository : CrudManagement, ITransactionRepository
    {
        public ITransactionRepository WithContext(ISqlConnectionManager sqlConnectionManager)
        {
            this.Transaction(sqlConnectionManager);
            return this;
        }
    }
}
