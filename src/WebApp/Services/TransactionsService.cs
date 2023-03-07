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
        public async Task<IEnumerable<Transaction>> GetTransactions(string username)
        {
            int userID = await _usersService.GetUserId(username);
            return await _httpClient.GetFromJsonAsync<Transaction[]>("/api/transactions/" + userID);
        }
        public async Task<Transaction> GetTransaction(string username, int transactionID)
        {
            int userID = await _usersService.GetUserId(username);
            return await _httpClient.GetFromJsonAsync<Transaction>("/api/transactions/" + userID + "/" + transactionID);
        }
    }
}
