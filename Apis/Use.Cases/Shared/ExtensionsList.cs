using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Use.Cases.Shared
{
    public static class ExtensionsList
    {
        public static PaginatedDto<TEntity> Pager<TEntity>(this IEnumerable<TEntity> items, int page = 0, int limit = 0, int totalRows = 0) where TEntity : class
        {
            limit = limit == 0 ? 1 : limit;
            decimal numberPages = Decimal.Divide(Convert.ToDecimal(totalRows), Convert.ToDecimal(limit));
            decimal remainder = numberPages / 1;
            var itemsCount = items?.Count() ?? 0;
            return new PaginatedDto<TEntity>()
            {
                CurrentPage = page == 0 ? 1 : page,
                HasElements = items?.Count() > 0,
                NumberPages = totalRows > 0 ? (remainder != 0 ? (int)numberPages + 1 : (int)numberPages) : 0,
                Limit = (itemsCount < limit ? itemsCount : limit),
                TotalRecords = totalRows,
                TotalRecordsPage = items?.Count() ?? 0,
                Elements = items
            };
        }
    }
}
