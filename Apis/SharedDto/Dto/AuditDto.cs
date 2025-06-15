namespace ManagementProducts.SharedDto.Dto
{
    public class AuditDto
    {
        public string Status { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedHost { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        public string? ModifiedBy { get; set; } = null;
        public string? ModifiedHost { get; set; } = null;
        public DateTime? ModifiedDate { get; set; } = null;
        public bool IsPaginated { get; set; } = false;
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 0;
    }
}
