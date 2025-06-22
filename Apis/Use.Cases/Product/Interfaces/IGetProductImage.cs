using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Product.Interfaces
{
    [ScopedService]
    public interface IGetProductImage
    {
        IGetProductImage WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<string, Failure> Execute(long id);
    }
}
