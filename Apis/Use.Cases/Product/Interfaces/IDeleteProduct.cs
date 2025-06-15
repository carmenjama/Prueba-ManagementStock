using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Product.Interfaces
{
    [ScopedService]
    public interface IDeleteProduct
    {
        IDeleteProduct WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<string, Failure> Execute(long id, string modifiedBy);
    }
}
