namespace AuthenticationService.Options
{
    public class ApiSetting
    {
        public const string SECTION = "ApiConfig";
        public string QueryKey { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string VerifyServiceEndpoint { get; set; } = string.Empty;
    }
}