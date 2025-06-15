using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Product.Interfaces
{
    [ScopedService]
    public interface IGetAllProduct
    {
        IGetAllProduct WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<PaginatedDto<ProductDto>, Failure> Execute(ProductDto dto);
    }
}
