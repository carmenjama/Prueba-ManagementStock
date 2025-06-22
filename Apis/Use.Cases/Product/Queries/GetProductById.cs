using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;

namespace ManagementProducts.Use.Cases.Product.Queries
{
    public class GetProductById : IGetProductById
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly IProductRepository _productRepository;

        public GetProductById(ISqlConnectionManager connectionManager,
            IProductRepository productRepository)
        {
            this._connectionManager = connectionManager;
            this._productRepository = productRepository;
        }

        public IGetProductById WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._productRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<ProductDto, Failure> Execute(long id)
        {
            try
            {
                if (id == 0)
                    return new UseCaseError { Reason = "Error", Message = "Id requerido" };
                var result = _productRepository.GetById(id);
                if (result is null)
                    return new NoResult { Reason = "Error", Message = "Registro no encontrado" };
                return new ProductDto
                {
                    Id = result.Id,
                    CategoryId = result.CategoryId,
                    Code = result.Code,
                    Name = result.Name,
                    Price = result.Price,
                    Unit = result.Unit,
                    Stock = result.Stock,
                    Data = result.Data,
                    Extension = result.Extension,
                    Note = result.Note,
                    Status = result.Status,
                    CreatedBy = result.CreatedBy,
                    CreatedHost = result.CreatedHost,
                    CreatedDate = result.CreatedDate,
                    ModifiedBy = result.ModifiedBy,
                    ModifiedHost = result.ModifiedHost,
                    ModifiedDate = result.ModifiedDate
                };
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error búsqueda datos" };
            }
        }
    }
}
