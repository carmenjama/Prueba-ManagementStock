namespace ManagementProducts.Use.Cases.Shared
{
    public class NoResult : Failure
    {
        public dynamic Reason { get; set; }
        public dynamic Message { get; set; }
    }
}
