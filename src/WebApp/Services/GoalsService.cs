using System.Text.Json;
using System.Text;
using WebApp.Models.Goals;

namespace WebApp.Services
{
    public class GoalsService
    {
        private readonly HttpClient _httpClient;
        public GoalsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async Task<IEnumerable<Goal>> GetGoals(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Goal[]>($"/api/goals/{userId}");
            }
            catch (Exception ex)
            {
                return new List<Goal>();
            }
        }
        public async Task<Goal> GetGoal(Guid userId, Guid goalId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Goal>($"/api/goals/{userId}/{goalId}");
            }
            catch (Exception ex)
            {
                return new Goal();
            }
        }
        public async Task<bool> AddGoal(NewGoal goal)
        {
            try
            {
                var content = await SerializeObj(goal);
                var res = await _httpClient.PostAsync("/api/goals", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateGoal(Goal goal)
        {
            try
            {
                var content = await SerializeObj(goal);
                var res = await _httpClient.PutAsync("/api/goals", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> DeleteGoal(Guid userId, Guid goalId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/goals/{userId}/{goalId}");
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
