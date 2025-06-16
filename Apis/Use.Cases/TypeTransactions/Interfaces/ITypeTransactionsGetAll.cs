using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.TypeTransactions.Interfaces
{
    [ScopedService] 
    public interface ITypeTransactionsGetAll
    {
        ITypeTransactionsGetAll WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<PaginatedDto<TypeTransactionDto>, Failure> Execute(TypeTransactionDto dto);
    }
}
