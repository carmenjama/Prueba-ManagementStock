using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.Master.Transaction;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.Transactions.Interfaces;

namespace ManagementProducts.Use.Cases.Transactions.Commands
{
    public class CreateTransaction : ICreateTransaction
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITypeTransactionRepository _typeTransactionRepository;

        public CreateTransaction(ISqlConnectionManager connectionManager,
            ITransactionRepository transactionRepository,
            IProductRepository productRepository,
            ITypeTransactionRepository typeTransactionRepository)
        {
            this._connectionManager = connectionManager;
            this._transactionRepository = transactionRepository;
            this._productRepository = productRepository;
            this._typeTransactionRepository = typeTransactionRepository;
        }

        public ICreateTransaction WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._transactionRepository.WithContext(_connectionManager);
            this._productRepository.WithContext(_connectionManager);
            this._typeTransactionRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<string, Failure> Execute(TransactionDto dto, string createdBy)
        {
            try
            {
                if (dto.ProductId == 0)
                    return new UseCaseError { Reason = "Error", Message = "Producto requerido" };
                if (string.IsNullOrEmpty(dto.TypeTransactionName))
                    return new UseCaseError { Reason = "Error", Message = "Tipo transacción requerido" };
                if (dto.Price == 0)
                    return new UseCaseError { Reason = "Error", Message = "Precio requerido" };
                if (dto.Quantity == 0)
                    return new UseCaseError { Reason = "Error", Message = "Cantidad requerido" };
                
                var type = _typeTransactionRepository.GetAll(new TypeTransactionDto { Name = dto.TypeTransactionName, Status = "ACTIVO", IsPaginated = false}, out int total);
                if (type is null || type.Count() == 0)
                    return new UseCaseError { Reason = "Error", Message = "Tipo transacción no encontrada" };

                var product = _productRepository.GetById(dto.ProductId);
                if (product is null)
                    return new UseCaseError { Reason = "Error", Message = $"Producto no encontrado" };
                if (dto.TypeTransactionName.Equals("VENTA") && product.Stock < dto.Quantity)
                    return new UseCaseError { Reason = "Error", Message = $"Stock {product.Stock} menor a cantidad {dto.Quantity} requerida " };

                var data = new TransactionTableRow
                {
                    ProductId = dto.ProductId,
                    TypeTransactionId = type?.FirstOrDefault()?.Id ?? 0,
                    Quantity = dto.Quantity,
                    Price = dto.Price,
                    TransactionDate = dto.TransactionDate,
                    Note = dto.Note,
                    
                };
                
                dto.Id = _transactionRepository.Insert(data.InsertAudit(createdBy));
                _productRepository.UpdateStock(dto.ProductId);
                if (dto.Id == 0)
                    return new UseCaseError { Reason = "Error", Message = "Error al ingresar datos" };
                return "Registro guardado";
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error al ingresar datos" };
            }
        }
    }
}
