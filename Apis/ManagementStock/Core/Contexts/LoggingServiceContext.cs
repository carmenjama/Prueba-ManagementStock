namespace ManagementProducts.Api.Core.Contexts
{
    public class LoggingServiceContext
    {
        public string Endpoint { set; get; } = string.Empty;
        public string[] SensitiveKeys { set; get; }
        public bool TraceActivities { set; get; }
        public bool TraceExceptions { set; get; }
        public bool ReturnErrorDataLog { set; get; }
    }
}
