namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public void UpdateStock(long id)
        {
            string query = @$"UPDATE [dbo].[Product]
            SET [Stock] = (Select ISNULL(SUM(Quantity),0) 
				            from [Transaction] T
				            inner join [TransactionType] TT ON TT.Id=T.TransactionTypeId
				            where T.ProductId=@Id AND TT.[Name]='COMPRA' AND T.[Status]='ACTIVO')
				            -
				            (Select ISNULL(SUM(Quantity),0) 
				            from [Transaction] T
				            inner join [TransactionType] TT ON TT.Id=T.TransactionTypeId
				            where T.ProductId=@Id AND TT.[Name]='VENTA' AND T.[Status]='ACTIVO')
            WHERE Id=@Id";
            this.Update(query, new{Id = id});
        }
    }
}
