
namespace ManagementProducts.SharedDto.Dto
{
    public class PaginatedDto<T>
    {
        public bool HasElements { get; set; } = false;
        public int NumberPages { get; set; } = 0;
        public int Limit { get; set; } = 0;
        public int TotalRecordsPage { get; set; } = 0;
        public int TotalRecords { get; set; } = 0;
        public int CurrentPage { get; set; } = 0;
        public IEnumerable<T> Elements { get; set; } = null;
    }
}
