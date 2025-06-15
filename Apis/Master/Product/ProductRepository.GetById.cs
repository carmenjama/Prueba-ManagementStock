using ManagementProducts.Master.Interfaces.TableRow;

namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public IProductTableRow GetById(long id)
        {
            string query = @$"SELECT 
                   P.[Id]
                  ,P.[CategoryId]
                  ,P.[Code]
                  ,P.[Name]
                  ,P.[Price]
                  ,P.[Unit]
                  ,P.[Stock]
                  ,P.[Data]
                  ,P.[Extension]
                  ,P.[Note]
                  ,P.[Status]
                  ,P.[CreatedBy]
                  ,P.[CreatedHost]
                  ,P.[CreatedDate]
                  ,P.[ModifiedBy]
                  ,P.[ModifiedHost]
                  ,P.[ModifiedDate]
              FROM [dbo].[Product] P
              WHERE P.[Id]=@id";

            return this.Get<ProductTableRow>(query, new{ id });
        }
    }
}
