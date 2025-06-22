using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Transactions.Interfaces
{
    [ScopedService]
    public interface ICreateTransaction
    {
        ICreateTransaction WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<string, Failure> Execute(TransactionDto dto, string createdBy);
    }
}
