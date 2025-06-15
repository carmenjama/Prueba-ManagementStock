namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public void Delete(long id, string status, string modifiedBy, string modifiedHost)
        {
            string query = @$"UPDATE [dbo].[Product]
               SET [Status] = @Status
                  ,[ModifiedBy] = @ModifiedBy
                  ,[ModifiedHost] = @ModifiedHost
                  ,[ModifiedDate] = @ModifiedDate
             WHERE Id=@Id";

            this.Update(query, 
                new { 
                    Id =  id, 
                    Status = status,
                    ModifiedBy = modifiedBy,
                    ModifiedHost = modifiedHost,
                    ModifiedDate = DateTime.Now
                });
        }
    }
}
