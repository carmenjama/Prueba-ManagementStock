using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Master.Interfaces
{
    [ScopedService]
    public interface ICategoryRepository : ICrudManagement
    {
        ICategoryRepository WithContext(ISqlConnectionManager sqlConnectionManager);
        IEnumerable<ICategoryTableRow> GetAll(CategoryDto dto, out int totalRows);
    }
}
