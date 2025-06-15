using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Master.Interfaces
{
    [ScopedService]
    public interface IProductRepository : ICrudManagement
    {
        IProductRepository WithContext(ISqlConnectionManager sqlConnectionManager);
        long Insert(IProductTableRow dto);
        void Update(IProductTableRow dto);
        bool Exists(string code);
        IProductTableRow GetById(long id);
        IEnumerable<IProductTableRow> GetAll(ProductDto dto, out int totalRows);
        void Delete(long id, string status, string modifiedBy, string modifiedHost);
    }
}
