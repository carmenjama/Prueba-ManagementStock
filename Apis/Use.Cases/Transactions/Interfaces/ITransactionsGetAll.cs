using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Transactions.Interfaces
{
    [ScopedService]
    public interface ITransactionsGetAll
    {
        ITransactionsGetAll WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<PaginatedDto<TransactionDto>, Failure> Execute(TransactionDto dto);
    }
}
