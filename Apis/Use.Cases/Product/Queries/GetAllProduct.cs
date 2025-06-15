using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;

namespace ManagementProducts.Use.Cases.Product.Queries
{
    public class GetAllProduct : IGetAllProduct
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly IProductRepository _productRepository;

        public GetAllProduct(ISqlConnectionManager connectionManager,
            IProductRepository productRepository)
        {
            this._connectionManager = connectionManager;
            this._productRepository = productRepository;
        }

        public IGetAllProduct WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._productRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<PaginatedDto<ProductDto>, Failure> Execute(ProductDto dto)
        {
            try
            {
                _productRepository.SetPaginated(dto.IsPaginated, dto.Page, dto.Limit);

                return _productRepository
                    .GetAll(dto, out int totalRows)?
                    .Select(x => new ProductDto
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        Code = x.Code,
                        Name = x.Name,
                        Price = x.Price,
                        Unit = x.Unit,
                        Stock = x.Stock,
                        Note = x.Note,
                        Status = x.Status,
                        CreatedBy = x.CreatedBy,
                        CreatedHost = x.CreatedHost,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedHost = x.ModifiedHost,
                        ModifiedDate = x.ModifiedDate,
                        HasMultimedia = x.HasMultimedia
                    }).Pager<ProductDto>(dto.Page, dto.Limit, totalRows);
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error búsqueda" };
            }
        }
    }
}