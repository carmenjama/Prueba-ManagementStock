using Dapper;
using ManagementProducts.Interfaces;

namespace ManagementProducts.Master.Shared
{
    public partial class CrudManagement
    {
        ISqlConnectionManager _sqlConnectionManager;
        public bool IsPaginated { get; set; } = false;
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 0;
        public int TotalRows { get; set; } = 0;
        public string OrderBy { get; set; } = string.Empty;

        public void Transaction(ISqlConnectionManager sqlConnectionManager)
        {
            try
            {
                this._sqlConnectionManager = sqlConnectionManager;
            }
            catch (Exception)
            {
                _sqlConnectionManager.RollbackTransaction();
                throw;
            }
        }

        public T Get<T>(string query, object values = null) where T : class
        {
            try
            {
                if (_sqlConnectionManager._sqlConnection != null)
                    return this._sqlConnectionManager._sqlConnection
                      .QueryFirstOrDefault<T>(query, values, this._sqlConnectionManager?._sqlTransaction);
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Exists(string query, object values = null)
        {
            try
            {
                if (_sqlConnectionManager._sqlConnection != null)
                {
                    var queryExists = $@"SELECT CASE WHEN EXISTS ( {query} ) THEN (SELECT 1) ELSE (SELECT 0) END";
                    return this._sqlConnectionManager._sqlConnection
                      .QueryFirstOrDefault<bool>(query, values, this._sqlConnectionManager?._sqlTransaction);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<T> GetAll<T>(string query, object values = null) where T : class
        {
            try
            {
                if (_sqlConnectionManager._sqlConnection != null)
                {
                    if (this.IsPaginated)
                    {
                        this.TotalRows = (int)this._sqlConnectionManager._sqlConnection
                        .Query(query, values)?.Count();
                        return this._sqlConnectionManager._sqlConnection
                        .Query<T>(PagerQuery(query), values);
                    }
                    else
                    {
                        return this._sqlConnectionManager._sqlConnection
                        .Query<T>(query, values, this._sqlConnectionManager?._sqlTransaction);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TType Insert<TType>(string query, object values)
        {
            try
            {
                return this._sqlConnectionManager._sqlConnection
                    .QuerySingleOrDefault<TType>(query, values, this._sqlConnectionManager._sqlTransaction);
            }
            catch (Exception ex)
            {
                this._sqlConnectionManager.RollbackTransaction();
                throw;
            }
        }

        public void Update(string query, object values)
        {
            try
            {
                this._sqlConnectionManager._sqlConnection
                    .Execute(query, values, this._sqlConnectionManager._sqlTransaction);
            }
            catch (Exception ex)
            {
                this._sqlConnectionManager.RollbackTransaction();
                throw;
            }
        }

        public void SetPaginated(bool isPaginated, int page, int limit)
        {
            this.IsPaginated = isPaginated;
            this.Page = page;
            this.Limit = limit;
        }

        private string PagerQuery(string query)
        {
            if (this.Page == 0 || this.Limit == 0)
                return query;
            return $"{query} OFFSET {this.Limit * (this.Page - 1)} ROWS FETCH NEXT {this.Limit} ROWS ONLY";
        }
    }
}
