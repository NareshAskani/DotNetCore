using DadJokesApp.Models;
using Newtonsoft.Json;

namespace DadJokesApp.Services
{
    public interface IDadJokesService
    {
        Task<int> GetJokesCountAsync();
        Task<List<DadJokesModel>> GetJokesAsync(int count);
    }

    public class DadJokesService : IDadJokesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DadJokesService> _logger;

        public DadJokesService(HttpClient httpClient, ILogger<DadJokesService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<int> GetJokesCountAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://dad-jokes.p.rapidapi.com/joke/count");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = !string.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<DadJokesResponse<int>>(content) : null;
                if (result != null && result.Success)
                {
                    return result.Body;
                }
                else
                {
                    return 0;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error getting jokes count from API");
            }
            return 0;
        }

        public async Task<List<DadJokesModel>> GetJokesAsync(int count)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://dad-jokes.p.rapidapi.com/random/joke?count={count}");

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var result = !string.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<DadJokesResponse<List<DadJokesModel>>>(content) : null;
                if (result!=null&&result.Success)
                {
                    return result.Body;
                }
                else {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error getting jokes from API");
                throw;
            }
        }
    }

}

