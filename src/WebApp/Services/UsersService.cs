using System.Text;
using System.Text.Json;
using WebApp.Models.Users;

namespace WebApp.Services
{
    public class UsersService
    {
        private readonly HttpClient _httpClient;
        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<User> SignIn(User user)
        {
            try
            {
                user.Id = Guid.Empty;
                user.Settings = String.Empty;
                user.Email = String.Empty;
                var content = await SerializeObj(user);
                var res = await _httpClient.PutAsync("/api/users/auth", content);
                var userInfo = await res.Content.ReadFromJsonAsync<User>();
                return userInfo;

            }
            catch (Exception ex)
            {
                return new User();
            }
        }
        public async Task<bool> UpdateUser(UpdateUser user)
        {
            try
            {
                var content = await SerializeObj(user);
                var res = await _httpClient.PutAsync("/api/users", content);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddUser(NewUser user)
        {
            try
            {
                var content = await SerializeObj(user);
                var res = await _httpClient.PostAsync("/api/users", content);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/users/{userId}");
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
