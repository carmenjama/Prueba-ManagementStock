using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Transactions.Interfaces
{
    [ScopedService] 
    public interface IDeleteTransaction
    {
        IDeleteTransaction WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<string, Failure> Execute(long id, string modifiedBy);
    }
}
