using ManagementProducts.Master.Interfaces.TableRow;

namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public long Insert(IProductTableRow dto)
        {
            string query = @$"INSERT INTO [dbo].[Product]
                   ([CategoryId]
                   ,[Code]
                   ,[Name]
                   ,[Price]
                   ,[Unit]
                   ,[Stock]
                   ,[Note]
                   ,[Data]
                   ,[Extension]
                   ,[Status]
                   ,[CreatedBy]
                   ,[CreatedHost]
                   ,[CreatedDate])
             VALUES
                   (@CategoryId
                   ,@Code
                   ,@Name
                   ,@Price
                   ,@Unit
                   ,@Stock
                   ,@Note
                   ,@Data
                   ,@Extension
                   ,@Status
                   ,@CreatedBy
                   ,@CreatedHost
                   ,@CreatedDate)
            SELECT @@IDENTITY";
            return this.Insert<long>(query,
              new
              {
                  CategoryId = dto.CategoryId,
                  Code = dto.Code,
                  Name = dto.Name,
                  Price = dto.Price,
                  Unit = dto.Unit,
                  Stock = 0,
                  Note = dto.Note,
                  Data = dto.Data,
                  Extension = dto.Extension,
                  Status = dto.Status,
                  CreatedBy = dto.CreatedBy,
                  CreatedHost = dto.CreatedHost,
                  CreatedDate = dto.CreatedDate
              });
        }
    }
}
