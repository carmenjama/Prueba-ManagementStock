namespace ManagementProducts.SharedDto.Dto
{
    public class CategoryDto: AuditDto
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}
