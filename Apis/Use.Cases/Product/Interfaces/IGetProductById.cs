using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Product.Interfaces
{
    [ScopedService]
    public interface IGetProductById
    {
        IGetProductById WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<ProductDto, Failure> Execute(long id);
    }
}
