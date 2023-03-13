using REST_API.Models.Users;
using System.Text.Json;
using System.Text;
using WebApp.Models;
namespace WebApp.Services
{
    public class TransactionsService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersService _usersService;
        public TransactionsService(HttpClient httpClient, UsersService usersService)
        {
            _httpClient = httpClient;
            _usersService = usersService;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async Task<IEnumerable<Transaction>> GetTransactions(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction[]>($"/api/transactions/{userId}");
            }
            catch (Exception ex)
            {
                return new List<Transaction>();
            }
        }
        public async Task<Transaction> GetTransaction(int userId, int transactionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction>($"/api/transactions/{userId}/" + transactionId);
            }
            catch (Exception ex)
            {
                return new Transaction();
            }
        }
        public async Task<bool> AddTransaction(int userId, Transaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PostAsync($"/api/transactions/{userId}/", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateTransaction(int userId, Transaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PutAsync($"/api/transactions/{userId}/", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> DeleteTransaction(int userId, int transactionId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/transactions/{userId}/{transactionId}");
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
