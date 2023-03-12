using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class UsersService
    {
        private readonly HttpClient _httpClient;
        private readonly User _emptyUser;
        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _emptyUser = new User();
        }
        //Deprecated
        public async Task<User> GetUser(int id)
        {
            try
            {
                User res = await _httpClient.GetFromJsonAsync<User>("/api/users/" + id);
                return res;
            }
            catch (Exception ex)
            {
                return new User();
            }
        }
        public async Task<User> SignIn(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var res = await _httpClient.PutAsync("/api/users/auth", content);
                var userInfo = await res.Content.ReadFromJsonAsync<User>();
                return userInfo;

            }
            catch (Exception ex)
            {
                return new User();
            }
        }
        public async Task<bool> UpdateUserInfo(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await _httpClient.PutAsync($"/api/users", content);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(res.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
