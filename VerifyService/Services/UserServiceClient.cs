using System.Net.Http;
using Microsoft.Extensions.Options;
using VerifyService.Options;
using VerifyService.Services.Interfaces;

namespace VerifyService.Services
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSetting _apiSetting;

        public UserServiceClient(HttpClient httpClient, IOptions<ApiSetting> apiSetting)
        {
            _httpClient = httpClient;
            _apiSetting = apiSetting.Value;
        }

        public async Task<bool> DoesUserExistAsync(string email, int? id = null)
        {
            string queryParams = string.Format("/api/user/api-key/exist?apiKey={0}&email={1}&id={2}", _apiSetting.Key, email, id ?? 0);
            return await CallToAuthService(queryParams);
        }

        private async Task<bool> CallToAuthService(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{_apiSetting.AuthServiceEndpoint}{endpoint}");
            return response.IsSuccessStatusCode && (await response.Content.ReadAsStringAsync()).ToLower() == "true";
        }
    }
}
