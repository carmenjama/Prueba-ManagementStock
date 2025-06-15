namespace ManagementProducts.Master.Shared
{
    public class AuditTableRow
    {
        public string Status { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedHost { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        public string? ModifiedBy { get; set; } = null;
        public string? ModifiedHost { get; set; } = null;
        public DateTime? ModifiedDate { get; set; } = null;
    }
}
