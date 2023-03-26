using WebApp.Models.Categories;

namespace WebApp.Services
{
    public class CategoriesService
    {
        private readonly HttpClient _httpClient;
        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Category[]>("/api/categories");
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }
        public async Task<Category> GetCategory(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Category>($"/api/categories/{id}");
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }
    }
}