using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using ManagementProducts.SharedDto.DbContext;
using Microsoft.Data.SqlClient;

namespace ManagementProducts.Interfaces
{
    [ScopedService]
    public interface ISqlConnectionManager
    {
        SqlConnection _sqlConnection { get; }
        SqlTransaction _sqlTransaction { get; }
        ISqlConnectionManager WithContext(SqlServerDbContext sqlServerDbContext);
        bool IsHealthy();
        void Disconnect();
        void StartTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
