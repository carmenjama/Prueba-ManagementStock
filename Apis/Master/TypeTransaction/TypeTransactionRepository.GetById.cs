using Dapper;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Master.TypeTransaction
{
    public partial class TypeTransactionRepository
    {
        public ITypeTransactionTableRow GetById(long id)
        {
            string query = @$"Select * 
                from TransactionType 
                WHERE Id = @Id
                order by CreatedDate desc";

            return this.Get<TypeTransactionTableRow>(query, new { Id = id });
        }
    }
}
