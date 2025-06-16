namespace ManagementProducts.SharedDto.Dto
{
    public class TypeTransactionDto : AuditDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}
