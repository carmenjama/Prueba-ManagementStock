namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public void UpdateStock(long id)
        {
            string query = @$"UPDATE [dbo].[Product]
                            SET [Stock] = (Select SUM(Quantity) from [Transaction] where ProductId=@Id AND [Status]='ACTIVO')
                            WHERE Id=@Id";
            this.Update(query, new{Id = id});
        }
    }
}
