using async.dto;
using async.Services.IServices;
using System.Text.Json;

namespace async.Services
{
    public class PostService : IPostService
    {
        private HttpClient _httpClient;
        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> Get()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);

            return post;
        }
    }
}
