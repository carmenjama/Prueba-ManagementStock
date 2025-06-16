using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementProducts.Use.Cases.Category.Interfaces
{
    [ScopedService]
    public interface IGetAllCategory
    {
        IGetAllCategory WithContext(SqlServerDbContext sqlServerDbContext);
        UseCaseResult<PaginatedDto<CategoryDto>, Failure> Execute(CategoryDto dto);
    }
}
