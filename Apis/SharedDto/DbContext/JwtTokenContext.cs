namespace ManagementProducts.SharedDto.DbContext
{
    public class JwtTokenContext
    {
        public string Key { set; get; } = string.Empty;
        public string AudienceToken { set; get; } = string.Empty;
        public string IsUserToken { set; get; } = string.Empty;
        public int ExpireMinutes { set; get; } = 0;
        public bool Expire { set; get; } = false;
        public string UserName { set; get; } = string.Empty;
        public string Pass { set; get; } = string.Empty;
    }
}
