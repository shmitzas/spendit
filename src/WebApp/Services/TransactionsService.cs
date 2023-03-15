using REST_API.Models.Users;
using System.Text.Json;
using System.Text;
using WebApp.Models;
using System.Collections.Generic;

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
                return await _httpClient.GetFromJsonAsync<Transaction>($"/api/transactions/{userId}/{transactionId}");
            }
            catch (Exception ex)
            {
                return new Transaction();
            }
        }
        public async Task<IEnumerable<Transaction>> Search(int userId, string query)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync <IEnumerable<Transaction>> ($"/api/transactions/{userId}/search={query}");
            }
            catch (Exception ex)
            {
                return new List<Transaction>();
            }
        }
        public async Task<bool> AddTransaction(Transaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PostAsync("/api/transactions", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateTransaction(Transaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PutAsync("/api/transactions", content);
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
