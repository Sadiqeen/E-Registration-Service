using System.Text;
using CourseService.Models;
using Newtonsoft.Json;

namespace CourseService.Utils
{
    public static class Validator
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> ValidateAsync<TRequest>(IConfiguration configuration,string endpoint, TRequest data, int? id = null)
        {
            var url = configuration["ApiConfig:VerifyServiceEndpoint"];
            var apiKey = configuration["ApiConfig:Key"];
            endpoint = $"{endpoint}?apiKey={apiKey}";

            if (id.HasValue)
            {
                endpoint = $"{endpoint}&id={id}";
            }

            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url + endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return ExtractErrorMessage(errorResponse);
            }

            return null;
        }

        private static string ExtractErrorMessage(string jsonResponse)
        {
            try
            {
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(jsonResponse);
                return errors[0]?.ErrorMessage; // Return the first error message
            }
            catch (JsonException)
            {
                return "An unknown error occurred.";
            }
        }
    }
}