using Dapper;
using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Master.Product
{
    public partial class ProductRepository
    {
        public IEnumerable<IProductTableRow> GetAll(ProductDto dto, out int totalRows)
        {
            DynamicParameters parameters = new DynamicParameters();
            string conditions = string.Empty;
            if (dto.Id is not  null && dto.Id !=0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Id]=@Id" : " AND P.[Id]=@Id";
                parameters.Add("Id", dto.Id);
            }
            if (dto.CategoryId != 0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[CategoryId]=@CategoryId" : " AND P.[CategoryId]=@CategoryId";
                parameters.Add("CategoryId", dto.CategoryId);
            }
            if (!string.IsNullOrEmpty(dto.Code))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Code]=@Code" : " AND P.[Code]=@Code";
                parameters.Add("Code", dto.Code);
            }
            if (!string.IsNullOrEmpty(dto.Name))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Name]=@Name" : " AND P.[Name]=@Name";
                parameters.Add("Name", dto.Name);
            }
            if (dto.Price != 0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Price]=@Price" : " AND P.[Price]=@Price";
                parameters.Add("Price", dto.Price);
            }
            if (!string.IsNullOrEmpty(dto.Unit))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Unit]=@Unit" : " AND P.[Unit]=@Unit";
                parameters.Add("Unit", dto.Unit);
            }
            if (dto.Stock != 0)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Stock]=@Stock" : " AND P.[Stock]=@Stock";
                parameters.Add("Stock", dto.Stock);
            }
            if (dto.FilterPriceMin is not null)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Price]>=@FilterPriceMin" : " AND P.[Price]>=@FilterPriceMin";
                parameters.Add("FilterPriceMin", dto.FilterPriceMin);
            }
            if (dto.FilterPriceMax is not null)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Price]<=@FilterPriceMax" : " AND P.[Price]<=@FilterPriceMax";
                parameters.Add("FilterPriceMax", dto.FilterPriceMax);
            }
            if (dto.FilterStockMin is not null)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Stock]>=@FilterStockMin" : " AND P.[Stock]>=@FilterStockMin";
                parameters.Add("FilterStockMin", dto.FilterStockMin);
            }
            if (dto.FilterStockMax is not null)
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Stock]<=@FilterStockMax" : " AND P.[Stock]<=@FilterStockMax";
                parameters.Add("FilterStockMax", dto.FilterStockMax);
            }
            if (!string.IsNullOrEmpty(dto.Status))
            {
                conditions += string.IsNullOrEmpty(conditions) ? " WHERE P.[Status]=@Status" : " AND P.[Status]=@Status";
                parameters.Add("Status", dto.Status);
            }

            string query = @$"SELECT 
                   P.[Id]
                  ,P.[CategoryId]
                  ,P.[Code]
                  ,P.[Name]
                  ,P.[Price]
                  ,P.[Unit]
                  ,P.[Stock]
                  ,P.[Note]
                  ,(CASE WHEN P.[Data] IS NULL THEN 0 ELSE 1 END) As HasMultimedia
                  ,C.[Name] As CategoryName
                  ,P.[Status]
                  ,P.[CreatedBy]
                  ,P.[CreatedHost]
                  ,P.[CreatedDate]
                  ,P.[ModifiedBy]
                  ,P.[ModifiedHost]
                  ,P.[ModifiedDate]
              FROM [dbo].[Product] P
              inner join [dbo].[Category] C on C.Id=P.CategoryId
              {conditions}
              ORDER BY P.[Status] asc, P.[CreatedDate] desc";

            var result = this.GetAll<ProductTableRow>(query, parameters);
            totalRows = this.TotalRows;
            return result;
        }
    }
}
