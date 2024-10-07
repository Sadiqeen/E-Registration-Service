namespace VerifyService.Options
{
    public class ApiSetting
    {
        public const string SECTION = "ApiConfig";
        public string QueryKey { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string AuthServiceEndpoint { get; set; } = string.Empty;
    }
}