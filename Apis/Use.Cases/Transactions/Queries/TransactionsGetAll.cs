using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.Transactions.Interfaces;

namespace ManagementProducts.Use.Cases.Transactions.Queries
{
    public class TransactionsGetAll : ITransactionsGetAll
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsGetAll(ISqlConnectionManager connectionManager,
            ITransactionRepository transactionRepository)
        {
            this._connectionManager = connectionManager;
            this._transactionRepository = transactionRepository;
        }

        public ITransactionsGetAll WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._transactionRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<PaginatedDto<TransactionDto>, Failure> Execute(TransactionDto dto)
        {
            try
            {
                _transactionRepository.SetPaginated(dto.IsPaginated, dto.Page, dto.Limit);

                return _transactionRepository
                    .GetAll(dto, out int totalRows)?
                    .Select(x => new TransactionDto
                    {
                        Id = x.Id,
                        TypeTransactionId = x.TypeTransactionId,
                        ProductName = x.ProductName,
                        TypeTransactionName = x.TypeTransactionName,
                        Status = x.Status,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Note = x.Note,
                        TransactionDate = x.TransactionDate,
                    }).Pager<TransactionDto>(dto.Page, dto.Limit, totalRows);
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error búsqueda" };
            }
        }
    }
}
