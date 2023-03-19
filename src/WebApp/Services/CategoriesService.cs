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
    }
}