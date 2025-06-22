using ManagementProducts.Interfaces;
using ManagementProducts.Master.Interfaces;
using ManagementProducts.SharedDto;
using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.SharedDto.Dto.Enums;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.Transactions.Interfaces;

namespace ManagementProducts.Use.Cases.Transactions.Commands
{
    public class DeleteTransaction : IDeleteTransaction
    {
        private readonly ISqlConnectionManager _connectionManager;
        private readonly ITransactionRepository _transactionRepository;

        public DeleteTransaction(ISqlConnectionManager connectionManager,
            ITransactionRepository transactionRepository)
        {
            this._connectionManager = connectionManager;
            this._transactionRepository = transactionRepository;
        }

        public IDeleteTransaction WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._connectionManager.WithContext(sqlServerDbContext);
            this._transactionRepository.WithContext(_connectionManager);
            return this;
        }

        public UseCaseResult<string, Failure> Execute(long id, string modifiedBy)
        {
            try
            {
                _transactionRepository.Delete(id, TypeState.Inactive.GetEnumDescription(), modifiedBy, Environment.MachineName);
                return "Registro eliminado";
            }
            catch (Exception ex)
            {
                return new UseCaseError { Reason = "Error", Message = "Error al ingresar datos" };
            }
        }
    }
}
