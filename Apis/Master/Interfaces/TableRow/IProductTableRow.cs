namespace ManagementProducts.Master.Interfaces.TableRow
{
    public interface IProductTableRow : IAuditTableRow
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public decimal Stock { get; set; }
        public byte[]? Data { get; set; }
        public string? Extension { get; set; }
        public string? Note { get; set; }
        public bool HasMultimedia { get; set; }
    }
}
