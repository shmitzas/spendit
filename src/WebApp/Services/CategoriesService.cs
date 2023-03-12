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
        public async Task<int> GetUserId(string username)
        {
            var res = await _httpClient.GetFromJsonAsync<Category>("/api/users/user/" + username);
            if (res == null)
            {
                return 0;
            }
            else
            {
                return (int)res.Id;
            }
        }
    }
}