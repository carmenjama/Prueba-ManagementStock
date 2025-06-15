namespace ManagementProducts.Use.Cases.Shared
{
    public class UseCaseError : Failure
    {
        public dynamic Reason { get; set; }
        public dynamic Message { get; set; }
        public dynamic Identifier { get; set; }
    }
}
