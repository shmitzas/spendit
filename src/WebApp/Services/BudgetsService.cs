using System.Text.Json;
using System.Text;
using WebApp.Models.Budgets;

namespace WebApp.Services
{
    public class BudgetsService
    {
        private readonly HttpClient _httpClient;
        public BudgetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async Task<IEnumerable<Budget>> GetBudgets(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Budget[]>($"/api/budgets/{userId}");
            }
            catch 
            {
                return new List<Budget>();
            }
        }
        public async Task<Budget> GetBudget(Guid userId, Guid BudgetId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Budget>($"/api/budgets/{userId}/{BudgetId}");
            }
            catch
            {
                return new Budget();
            }
        }
        public async Task<Budget> GetActiveBudget(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Budget>($"/api/budgets/active/{userId}");
            }
            catch
            {
                return new Budget();
            }
        }
        public async Task<bool> AddBudget(NewBudget Budget)
        {
            try
            {
                var content = await SerializeObj(Budget);
                var res = await _httpClient.PostAsync("/api/budgets", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<bool> UpdateBudget(Budget Budget)
        {
            try
            {
                var content = await SerializeObj(Budget);
                var res = await _httpClient.PutAsync("/api/budgets", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<bool> DeleteBudget(Guid userId, Guid BudgetId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/budgets/{userId}/{BudgetId}");
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SetActiveBudget(Budget budget)
        {
            try
            {
                var content = await SerializeObj(budget);
                var res = await _httpClient.PutAsync($"/api/budgets/activate", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> SetInactiveBudget(Budget budget)
        {
            try
            {
                var content = await SerializeObj(budget);
                var res = await _httpClient.PutAsync($"/api/budgets/deactivate", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
