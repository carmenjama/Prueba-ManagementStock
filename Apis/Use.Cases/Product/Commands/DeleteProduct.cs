using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto.Enums;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;

namespace ManagementProducts.Use.Cases.Product.Commands
{
    public class DeleteProduct : IDeleteProduct
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly IProductRepository _productRepository;

        public DeleteProduct(ISqlConnectionManager connectionManager,
            IProductRepository productRepository)
        {
            this._connectionManager = connectionManager;
            this._productRepository = productRepository;
        }

        public IDeleteProduct WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._productRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<string, Failure> Execute(long id, string modifiedBy)
        {
            try
            {
                _productRepository.Delete(id, TypeState.Inactive.GetEnumDescription(), modifiedBy, Environment.MachineName);
                _productRepository.UpdateStock(id);
                return "Registro eliminado";
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error al ingresar datos" };
            }
        }
    }
}
