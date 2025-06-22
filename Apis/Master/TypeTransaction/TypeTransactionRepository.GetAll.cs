using Dapper;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Master.TypeTransaction
{
    public partial class TypeTransactionRepository
    {
        public IEnumerable<ITypeTransactionTableRow> GetAll(TypeTransactionDto dto, out int totalRows)
        {
            DynamicParameters parameters = new DynamicParameters();
            string conditions = string.Empty;
            if (dto.Id != 0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE [Id]=@Id" : " AND [Id]=@Id";
                parameters.Add("Id", dto.Id);
            }
            if (!string.IsNullOrEmpty(dto.Name))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE [Name]=@Name" : " AND [Name]=@Name";
                parameters.Add("Name", dto.Name);
            }
            if (!string.IsNullOrEmpty(dto.Status))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE [Status]=@Status" : " AND [Status]=@Status";
                parameters.Add("Status", dto.Status);
            }

            string query = @$"Select * 
                from TransactionType 
                {conditions}
                order by CreatedDate desc";

            var result = this.GetAll<TypeTransactionTableRow>(query, parameters);
            totalRows = this.TotalRows;
            return result;
        }
    }
}
