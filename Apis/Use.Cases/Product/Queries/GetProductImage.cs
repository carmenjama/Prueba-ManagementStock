using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using System.Buffers.Text;
using System.Collections;

namespace ManagementProducts.Use.Cases.Product.Queries
{
    public class GetProductImage : IGetProductImage
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly IProductRepository _productRepository;

        public GetProductImage(ISqlConnectionManager connectionManager,
            IProductRepository productRepository)
        {
            this._connectionManager = connectionManager;
            this._productRepository = productRepository;
        }

        public IGetProductImage WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._productRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<string, Failure> Execute(long id)
        {
            try
            {
                if (id == 0)
                    return new UseCaseError { Reason = "Error", Message = "Id requerido" };
                var result = _productRepository.GetImage(id);
                return $"data:{result.Type};base64,{Convert.ToBase64String(result.Data)}";
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error al obtener datos" };
            }
        }
    }
}
