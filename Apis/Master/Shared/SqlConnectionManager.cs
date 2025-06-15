using ManagementProducts.SharedDto.DbContext;
using ManagementProducts.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ManagementProducts.Shared
{
    public class SqlConnectionManager : ISqlConnectionManager
    {
        protected SqlServerDbContext _sqlServerDbContext;
        public SqlConnection _sqlConnection { get; set; }
        public SqlTransaction _sqlTransaction { get; set; }

        public SqlConnection SqlConnection
        {
            get { return _sqlConnection; }
        }

        public SqlTransaction SqlTransaction
        {
            get { return _sqlTransaction; }
        }

        public ISqlConnectionManager WithContext(SqlServerDbContext sqlServerDbContext)
        {
            this._sqlServerDbContext = sqlServerDbContext;
            this.Connect();
            return this;
        }

        public void RollbackTransaction()
        {
            try
            {
                if (this._sqlTransaction != null)
                    this._sqlTransaction.Rollback();

            }
            catch (Exception ex)
            {

            }
        }

        public void Disconnect()
        {
            if (this._sqlConnection != null)
            {
                this._sqlConnection.Dispose();
                this._sqlConnection.Close();
            }
        }

        public bool IsHealthy()
        {
            bool success;
            using (this._sqlConnection = new SqlConnection(_sqlServerDbContext.ConnectionString))
            {
                try
                {
                    this._sqlConnection.Open();
                    success = true;
                }
                catch (Exception) { success = false; }
                finally { if (this._sqlConnection.State != ConnectionState.Closed) this._sqlConnection.Close(); }
            }
            return success;
        }

        public void StartTransaction()
        {
            this._sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                if (this._sqlTransaction != null)
                    this._sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw;
            }
            finally
            {
                this.EndTransaction();
                this.Disconnect();
            }
        }

        private void EndTransaction()
        {
            try
            {
                if (this._sqlTransaction != null)
                    this._sqlTransaction.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void Connect()
        {
            this._sqlConnection = new SqlConnection(_sqlServerDbContext.ConnectionString);
            this._sqlConnection.Open();
        }
    }
}
