namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public void UpdateStock(long id)
        {
            string query = @$"UPDATE [dbo].[Product]
                            SET [Stock] = (Select ISNULL(SUM(Quantity),0) from [Transaction] where ProductId=@Id AND [Status]='ACTIVO')
                            WHERE Id=@Id";
            this.Update(query, new{Id = id});
        }
    }
}
