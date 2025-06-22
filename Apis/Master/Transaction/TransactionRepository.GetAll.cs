using Dapper;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Master.Transaction
{
    public partial class TransactionRepository
    {
        public IEnumerable<ITransactionTableRow> GetAll(TransactionDto dto, out int totalRows)
        {
            DynamicParameters parameters = new DynamicParameters();
            string conditions = string.Empty;
            if (dto.Id != 0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE T.[Id]=@Id" : " AND T.[Id]=@Id";
                parameters.Add("Id", dto.Id);
            }
            if (dto.TypeTransactionId != 0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE T.[TransactionTypeId]=@TypeTransactionId" : " AND T.[TransactionTypeId]=@TypeTransactionId";
                parameters.Add("TypeTransactionId", dto.TypeTransactionId);
            }
            if (!string.IsNullOrEmpty(dto.ProductName))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Name]=@ProductName" : " AND P.[Name]=@ProductName";
                parameters.Add("ProductName", dto.ProductName);
            }
            if (dto.FilterMinDate is not null)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE T.[Date]>=@FilterMinDate" : " AND T.[Date]>=@FilterMinDate";
                parameters.Add("FilterMinDate", dto.FilterMinDate);
            }
            if (dto.FilterMaxDate is not null)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE T.[Date]>=@FilterMaxDate" : " AND T.[Date]>=@FilterMaxDate";
                parameters.Add("FilterMaxDate", dto.FilterMaxDate);
            }
            if (!string.IsNullOrEmpty(dto.Status))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE T.[Status]=@Status" : " AND T.[Status]=@Status";
                parameters.Add("Status", dto.Status);
            }

            string query = @$"Select 
                            T.[Id] As [Id],
                            T.[TransactionTypeId] As [TypeTransactionId],
                            P.[Name] As [ProductName],
                            TT.[Name] As TypeTransactionName,
                            T.[Status] As [Status],
                            T.[Quantity] As [Quantity],
                            T.[Price] As [Price],
                            T.[Date] As [TransactionDate]
                            from [Transaction] T 
                            inner join [Product] P ON P.Id=T.ProductId
                            inner join [TransactionType] TT ON TT.Id=T.TransactionTypeId 
                            {conditions}
                            order by T.[Date] desc, T.[Status] Asc";

            var result = this.GetAll<TransactionTableRow>(query, parameters);
            totalRows = this.TotalRows;
            return result;
        }

    }
}