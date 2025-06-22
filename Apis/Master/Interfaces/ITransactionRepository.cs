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
        IEnumerable<ITransactionTableRow> GetAll(TransactionDto dto, out int totalRows);
        long Insert(ITransactionTableRow dto);
    }
}
