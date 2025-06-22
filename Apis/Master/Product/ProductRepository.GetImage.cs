namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public (byte[] Data, string Type) GetImage(long id)
        {
            string query = @$"SELECT 
              P.[Data] As Data,
              P.[Extension] As Type
              FROM [dbo].[Product] P
              WHERE P.[Id]=@id";

            var result = this.Get<dynamic>(query, new { id });
            return (result?.Data, result?.Type);
        }
    }
}
