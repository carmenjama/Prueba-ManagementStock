using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.Master.Product;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;

namespace ManagementProducts.Use.Cases.Product.Commands
{
    public class UpdateProduct : IUpdateProduct
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly IProductRepository _productRepository;

        public UpdateProduct(ISqlConnectionManager connectionManager,
            IProductRepository productRepository)
        {
            this._connectionManager = connectionManager;
            this._productRepository = productRepository;
        }

        public IUpdateProduct WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._productRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<string, Failure> Execute(ProductDto dto, string modifiedBy)
        {
            try
            {
                if (dto.Id == 0)
                    return new UseCaseError { Reason = "Error", Message = "Id requerido" };
                if (string.IsNullOrEmpty(dto.Name))
                    return new UseCaseError { Reason = "Error", Message = "Nombre requerido" };
                if (string.IsNullOrEmpty(dto.Code))
                    return new UseCaseError { Reason = "Error", Message = "Código requerido" };
                if (dto.CategoryId == 0)
                    return new UseCaseError { Reason = "Error", Message = "Categoría requerida" };
                if (dto.Price == 0)
                    return new UseCaseError { Reason = "Error", Message = "Precio requerido" };
                if (string.IsNullOrEmpty(dto.Unit))
                    return new UseCaseError { Reason = "Error", Message = "Unidad requerida" };
                if (string.IsNullOrEmpty(dto.CreatedBy))
                    return new UseCaseError { Reason = "Error", Message = "Usuario requerido" };

                var data = new ProductTableRow
                {
                    Id = dto.Id ?? 0,
                    CategoryId = dto.CategoryId,
                    Code = dto.Code,
                    Name = dto.Name,
                    Price = dto.Price,
                    Unit = dto.Unit,
                    Stock = dto.Stock,
                    Note = dto.Note
                };
                if (!string.IsNullOrEmpty(dto.Multimedia))
                {
                    var multimedia = Helper.GetByteMultimedia(dto.Multimedia);
                    data.Data = multimedia.data;
                    data.Extension = multimedia.type;
                }
                _productRepository.Update(data.UpdateAudit(modifiedBy));
                _productRepository.UpdateStock(data.Id);
                return "Registro actualizado";
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error al actualizar datos" };
            }
        }
    }
}
