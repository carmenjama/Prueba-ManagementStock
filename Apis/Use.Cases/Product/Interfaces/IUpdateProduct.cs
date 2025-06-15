using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Product.Interfaces
{
    [ScopedService]
    public interface IUpdateProduct
    {
        IUpdateProduct WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<string, Failure> Execute(ProductDto dto, string modifiedBy);
    }
}
