using WebApp.Models;

namespace WebApp.Services
{
    public class TransactionsService
    {
        private readonly HttpClient _httpClient;
        public TransactionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Transaction>> GetTransactions(int userID)
        {
            return await _httpClient.GetFromJsonAsync<Transaction[]>("/api/transactions/" + userID);
        }
        public async Task<Transaction> GetTransaction(int userID, int transactionID)
        {
            return await _httpClient.GetFromJsonAsync<Transaction>("/api/transactions/" + userID + "/" + transactionID);
        }
    }
}
