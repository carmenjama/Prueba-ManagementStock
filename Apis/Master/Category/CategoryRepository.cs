using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.Master.Shared;

namespace ManagementProducts.Master.Category
{
    public partial class CategoryRepository : CrudManagement, ICategoryRepository
    {
        public ICategoryRepository WithContext(ISqlConnectionManager sqlConnectionManager)
        {
            this.Transaction(sqlConnectionManager);
            return this;
        }
    }
}
