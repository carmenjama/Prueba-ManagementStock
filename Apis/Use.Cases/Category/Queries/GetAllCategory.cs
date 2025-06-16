using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Category.Interfaces;
using ManagementProducts.Use.Cases.Shared;

namespace ManagementProducts.Use.Cases.Category.Queries
{
    public class GetAllCategory : IGetAllCategory
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategory(ISqlConnectionManager connectionManager,
            ICategoryRepository categoryRepository)
        {
            this._connectionManager = connectionManager;
            this._categoryRepository = categoryRepository;
        }

        public IGetAllCategory WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._categoryRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<PaginatedDto<CategoryDto>, Failure> Execute(CategoryDto dto)
        {
            try
            {
                _categoryRepository.SetPaginated(dto.IsPaginated, dto.Page, dto.Limit);

                return _categoryRepository
                    .GetAll(dto, out int totalRows)?
                    .Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).Pager<CategoryDto>(dto.Page, dto.Limit, totalRows);
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error búsqueda" };
            }
        }
    }
}
