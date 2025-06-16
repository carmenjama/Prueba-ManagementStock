using ManagementProducts.Master.Interfaces.TableRow;

namespace ManagementProducts.Master.Category
{
    public class CategoryTableRow : ICategoryTableRow
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}
