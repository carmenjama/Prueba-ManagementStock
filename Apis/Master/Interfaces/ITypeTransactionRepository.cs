using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Master.Interfaces
{
    [ScopedService]
    public interface ITypeTransactionRepository : ICrudManagement
    {
        ITypeTransactionRepository WithContext(ISqlConnectionManager sqlConnectionManager);
        ITypeTransactionTableRow GetById(long id);
        IEnumerable<ITypeTransactionTableRow> GetAll(TypeTransactionDto dto, out int totalRows);
    }
}
