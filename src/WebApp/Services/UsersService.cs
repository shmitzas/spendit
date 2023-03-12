using Azure;
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
        public async Task<int> GetUserId(string username)
        {
            try
            {
                User res = await _httpClient.GetFromJsonAsync<User>("/api/users/user/" + username);
                return (int)res.Id;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public async Task<User> GetUser(int userId)
        {
            // refactorinti, kad get user veiktu viduje API (SignIn funkcija turi grazint ne ID, o visa user obj)
            try
            {
                User res = await _httpClient.GetFromJsonAsync<User>("/api/users/" + userId);
                return res;
            }
            catch (Exception ex)
            {
                ////Console.WriteLine(ex.Message);
                return _emptyUser;
            }
        }
        public async Task<User> SignIn(User user)
        {
            try
            {
                /*
                string credentials = username + " " + password;
                var response = await _httpClient.GetAsync("/api/users/auth/" + credentials);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<User>(response.Content.ToString());
                    return (int)result.Id;
                }
                return 0;

            
                User res = await _httpClient.GetFromJsonAsync<User>("/api/users/auth/" + credentials);
                return (int)res.Id;
                */

                //NEW:
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var res = await _httpClient.PutAsync("/api/users/auth/", content);
                var responseString = await res.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<User>(responseString);
                return userInfo;

            }
            catch (Exception ex)
            {
                ////Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public async Task<bool> UpdateUserInfo(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await _httpClient.PutAsync($"/api/users/", content);
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
