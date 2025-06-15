using ManagementProducts.SharedDto.Dto;

namespace ManagementProducts.Api.Core.Shared
{
    public static class ExtensionObject
    {
        public static PaginatedResponse<T> PagerObject<TDto, T>(this IEnumerable<T> items, PaginatedDto<TDto> pager)
        {
            return new PaginatedResponse<T>
            {
                Elements = items,
                CurrentPage = pager.CurrentPage,
                HasElements = pager.HasElements,
                NumberPages = pager.NumberPages,
                TotalRecords = pager.TotalRecords,
                TotalRecordsPage = pager.TotalRecordsPage,
                Limit = pager.Limit,
            };
        }
    }
}
