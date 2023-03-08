using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class UsersService
    {
        private readonly HttpClient _httpClient;
        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> GetUserId(string username)
        {
            var res = await _httpClient.GetFromJsonAsync<User>("/api/users/user/" + username);
            if (res == null)
            {
                return 0;
            }
            else
            {
                return (int)res.Id;
            }
        }
        public async Task<User> GetUser(int userId)
        {
            var res = await _httpClient.GetFromJsonAsync<User>("/api/users/" + userId);
            if (res == null)
            {
                return new User();
            }
            else
            {
                return res;
            }
        }
        public async Task<int> SignIn(string username, string password)
        {
            string credentials = username + " " + password;
            //var response = await _httpClient.GetAsync("/api/users/auth/" + credentials);
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = JsonSerializer.Deserialize<User>(response.Content.ToString());
            //    return (int)result.Id;
            //}
            //return 0;

            //OLD:
            var res = await _httpClient.GetFromJsonAsync<User>("/api/users/auth/" + credentials);
            if (res != null)
            {
                return (int)res.Id;
            }
            return 0;
        }
        public async Task<bool> UpdateUserInfo(User user)
        {
            string json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _httpClient.PutAsJsonAsync("/api/users/", content);
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
