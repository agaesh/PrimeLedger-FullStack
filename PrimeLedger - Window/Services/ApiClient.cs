
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

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

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync(_baseUrl + endpoint, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"POST failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
    }

}
