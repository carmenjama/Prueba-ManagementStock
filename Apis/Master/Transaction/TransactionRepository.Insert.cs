using ManagementProducts.Master.Interfaces.TableRow;

namespace ManagementProducts.Master.Transaction
{
    public partial class TransactionRepository
    {
        public long Insert(ITransactionTableRow dto)
        {
            string query = @$"INSERT INTO [dbo].[Transaction]
                   ([ProductId]
                   ,[TransactionTypeId]
                   ,[Quantity]
                   ,[Price]
                   ,[Date]
                   ,[Note]
                   ,[Status]
                   ,[CreatedBy]
                   ,[CreatedHost]
                   ,[CreatedDate])
             VALUES
                   (@ProductId
                   ,@TransactionTypeId
                   ,@Quantity
                   ,@Price
                   ,@Date
                   ,@Note
                   ,@Status
                   ,@CreatedBy
                   ,@CreatedHost
                   ,@CreatedDate)
            SELECT @@IDENTITY";

            return this.Insert<long>(query, new {
                ProductId = dto.ProductId,
                TransactionTypeId = dto.TypeTransactionId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                Date = dto.TransactionDate,
                Note = dto.Note,
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                CreatedHost = dto.CreatedHost
            });
        }

    }
}