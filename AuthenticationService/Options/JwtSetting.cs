namespace AuthenticationService.Options
{
    public class JwtSetting
    {
        public const string SECTION = "JwtConfig";

        public string Secret { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }
}