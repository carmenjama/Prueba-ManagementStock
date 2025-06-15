namespace ManagementProducts.Master.Interfaces.TableRow
{
    public interface IAuditTableRow
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedHost { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedHost { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
