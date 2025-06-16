using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.TypeTransactions.Interfaces;

namespace ManagementProducts.Use.Cases.TypeTransactions.Queries
{
    internal class TypeTransactionsGetAll : ITypeTransactionsGetAll
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly ITransactionRepository _transactionRepository;

        public TypeTransactionsGetAll(ISqlConnectionManager connectionManager,
            ITransactionRepository transactionRepository)
        {
            this._connectionManager = connectionManager;
            this._transactionRepository = transactionRepository;
        }

        public ITypeTransactionsGetAll WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._transactionRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<PaginatedDto<TypeTransactionDto>, Failure> Execute(TypeTransactionDto dto)
        {
            try
            {
                _transactionRepository.SetPaginated(dto.IsPaginated, dto.Page, dto.Limit);

                return _transactionRepository
                    .GetAll(dto, out int totalRows)?
                    .Select(x => new TypeTransactionDto
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).Pager<TypeTransactionDto>(dto.Page, dto.Limit, totalRows);
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error búsqueda" };
            }
        }
    }
}
