
using System.Configuration;
using Newtonsoft.Json;

namespace PrimeLedger___Window.Services
{
    public class ApiClient
    {
        private readonly HttpClient _client;
        private readonly string? _baseUrl;

        public ApiClient()
        {
            _client = new HttpClient();
            _baseUrl = ConfigurationManager.AppSettings["BaseURL"];
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(_baseUrl + endpoint);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"GET failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
