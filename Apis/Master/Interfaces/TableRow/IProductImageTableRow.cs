namespace ManagementProducts.Master.Interfaces.TableRow
{
    public interface IProductImageTableRow : IAuditTableRow
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte[]? Data { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
    }
}
