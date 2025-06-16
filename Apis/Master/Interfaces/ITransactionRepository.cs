using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Master.Interfaces
{
    [ScopedService]
    public interface ITransactionRepository : ICrudManagement
    {
        ITransactionRepository WithContext(ISqlConnectionManager sqlConnectionManager);
        IEnumerable<ITypeTransactionTableRow> GetAll(TypeTransactionDto dto, out int totalRows);
    }
}
