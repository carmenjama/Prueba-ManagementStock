using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.Master.Shared;

namespace ManagementProducts.Master.TypeTransaction
{
    public partial class TypeTransactionRepository : CrudManagement, ITypeTransactionRepository
    {
        public ITypeTransactionRepository WithContext(ISqlConnectionManager sqlConnectionManager)
        {
            this.Transaction(sqlConnectionManager);
            return this;
        }
    }
}
