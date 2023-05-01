using System.Text.Json;
using System.Text;
using WebApp.Models.Transactions;

namespace WebApp.Services
{
    public class TransactionsService
    {
        private readonly HttpClient _httpClient;
        public TransactionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async Task<IEnumerable<Transaction>> GetTransactions(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction[]>($"/api/transactions/{userId}");
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<Transaction> GetTransaction(Guid userId, Guid transactionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction>($"/api/transactions/{userId}/{transactionId}");
            }
            catch
            {
                return new Transaction();
            }
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsByDate(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction[]>($"/api/transactions/{userId}/filter/start={startDate.ToString("yyyy-MM-dd")}&end={endDate.ToString("yyyy-MM-dd")}");
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsByDateAndCategory(Guid userId, DateTime startDate, DateTime endDate, string categoryName)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction[]>($"/api/transactions/{userId}/filter/start={startDate.ToString("yyyy-MM-dd")}&end={endDate.ToString("yyyy-MM-dd")}/category={categoryName}");
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsByCategory(Guid userId, string categoryName)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction[]>($"/api/transactions/{userId}/filter/category={categoryName}");
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsByBudget(Guid userId, Guid budgetId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Transaction[]>($"/api/transactions/{userId}/budget={budgetId}");
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<IEnumerable<Transaction>> Search(Guid userId, string query)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync <IEnumerable<Transaction>> ($"/api/transactions/{userId}/search={query}");
            }
            catch
            {
                return new List<Transaction>();
            }
        }
        public async Task<bool> AddTransaction(NewTransaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PostAsync("/api/transactions", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateTransaction(UpdateTransaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PutAsync("/api/transactions", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> DeleteTransaction(Guid userId, Guid transactionId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/transactions/{userId}/{transactionId}");
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
