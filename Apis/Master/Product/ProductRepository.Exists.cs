namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public bool Exists(string code)
        {
            string query = @$"SELECT P.[Id] FROM [dbo].[Product] P WHERE P.[Code]=@code";

            return this.Exists(query, new { code });
        }
    }
}
