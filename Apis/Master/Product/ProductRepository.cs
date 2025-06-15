using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.Master.Shared;

namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository : CrudManagement, IProductRepository
    {
        public IProductRepository WithContext(ISqlConnectionManager sqlConnectionManager)
        {
            this.Transaction(sqlConnectionManager);
            return this;
        }
    }
}
