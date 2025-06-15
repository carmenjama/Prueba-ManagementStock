using ManagementProducts.Master.Interfaces.TableRow;

namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public void Update(IProductTableRow dto)
        {
            string query = @$"UPDATE [dbo].[Product]
               SET [CategoryId] = @CategoryId
                  ,[Code] = @Code
                  ,[Name] = @Name
                  ,[Price] = @Price
                  ,[Unit] = @Unit
                  ,[Stock] = @Stock
                  ,[Note] = @Note
                  ,[Status] = @Status
                  ,[ModifiedBy] = @ModifiedBy
                  ,[ModifiedHost] = @ModifiedHost
                  ,[ModifiedDate] = @ModifiedDate
             WHERE Id=@Id";
            this.Update(query,
                new
                {
                    Id = dto.Id,
                    CategoryId = dto.CategoryId,
                    Code = dto.Code,
                    Name = dto.Name,
                    Price = dto.Price,
                    Unit = dto.Unit,
                    Stock = dto.Stock,
                    Note = dto.Note,
                    Status = dto.Status,
                    ModifiedBy = dto.ModifiedBy,
                    ModifiedHost = dto.ModifiedHost,
                    ModifiedDate = dto.ModifiedDate
                });
        }
    }
}
