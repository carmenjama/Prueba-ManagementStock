using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Product.Interfaces
{
    [ScopedService]
    public interface ICreateProduct
    {
        ICreateProduct WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<string, Failure> Execute(ProductDto dto, string createdBy);
    }
}
