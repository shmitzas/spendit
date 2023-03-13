using System.Text;
using System.Text.Json;
using WebApp.Models;
using System.Text.RegularExpressions;

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
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                user = await SanitizeUpdatedUser(user);
                var content = await SerializeObj(user);
                Console.Write($"\n\n\n{content}");
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
        public async Task<bool> AddUser(User user)
        {
            try
            {
                user = await SanitizeNewUser(user);
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
        public async Task<bool> DeleteUser(int userId)
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

        private async Task<User> SanitizeNewUser(User user)
        {
            user.Username = await SanitizeUsername(user.Username);
            user.Password = await SanitizePassword(user.Password);
            user.Email = await SanitizeEmail(user.Email);
            return user;
        }
        private async Task<User> SanitizeUpdatedUser(User user)
        {
            user.Password = await SanitizePassword(user.Password);
            user.Email = await SanitizeEmail(user.Email);
            return user;
        }

        //Allows only letters and numbers
        private async Task<string> SanitizeUsername(string input)
        {
            return Regex.Replace(input, @"[^a-zA-Z0-9]", "");
        }

        /*
         Allowed special characters are
         !@#$%^&*()-+=[]{};:'",.<>/?\|.
        */
        private async Task<string> SanitizePassword(string input)
        {
            return Regex.Replace(input, @"[^\w!@#$%^&*()\-+=\[\]{};:'"",.<>/?\\|]", "");
        }

        //Allows only characters used for email address
        private async Task<string> SanitizeEmail(string input)
        {
            return Regex.Replace(input, @"[^a-zA-Z0-9!@#$%^&*()\-+=\[\]{};:'"",.<>/?\\|]+$", "");
        }
        
    }
}
