using WebApp.Models;

namespace WebApp.Services
{
    public class CategoriesService
    {
        private readonly HttpClient _httpClient;
        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> GetUserId(string username)
        {
            var res = await _httpClient.GetFromJsonAsync<Category>("/api/users/user/" + username);
            if (res == null)
            {
                return Guid.Empty;
            }
            else
            {
                return res.Id;
            }
        }
    }
}