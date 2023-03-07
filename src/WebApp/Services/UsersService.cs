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
            var res = await _httpClient.GetFromJsonAsync<User>("/api/users/auth/" + credentials);
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
