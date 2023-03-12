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
        public async Task<IEnumerable<Transaction>> GetTransactions(int userId)
        {
            return await _httpClient.GetFromJsonAsync<Transaction[]>("/api/transactions/" + userId);
        }
        public async Task<Transaction> GetTransaction(int userId, int transactionId)
        {
            return await _httpClient.GetFromJsonAsync<Transaction>("/api/transactions/" + userId + "/" + transactionId);
        }
    }
}
