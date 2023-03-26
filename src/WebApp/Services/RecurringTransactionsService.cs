using System.Text.Json;
using System.Text;
using WebApp.Models.RecurringTransactions;

namespace WebApp.Services
{
    public class RecurringTransactionsService
    {
        private readonly HttpClient _httpClient;
        public RecurringTransactionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async Task<IEnumerable<RecurringTransaction>> GetTransactions(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecurringTransaction[]>($"/api/recurringtransactions/{userId}");
            }
            catch (Exception ex)
            {
                return new List<RecurringTransaction>();
            }
        }
        public async Task<RecurringTransaction> GetTransaction(Guid userId, Guid transactionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecurringTransaction>($"/api/recurringtransactions/{userId}/{transactionId}");
            }
            catch (Exception ex)
            {
                return new RecurringTransaction();
            }
        }
        public async Task<IEnumerable<RecurringTransaction>> GetTransactionsByDate(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecurringTransaction[]>($"/api/recurringtransactions/{userId}/filter/start={startDate.ToString("yyyy-MM-dd")}&end={endDate.ToString("yyyy-MM-dd")}");
            }
            catch (Exception ex)
            {
                return new List<RecurringTransaction>();
            }
        }
        public async Task<IEnumerable<RecurringTransaction>> GetTransactionsByDateAndCategory(Guid userId, DateTime startDate, DateTime endDate, string categoryName)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecurringTransaction[]>($"/api/recurringtransactions/{userId}/filter/start={startDate.ToString("yyyy-MM-dd")}&end={endDate.ToString("yyyy-MM-dd")}/category={categoryName}");
            }
            catch (Exception ex)
            {
                return new List<RecurringTransaction>();
            }
        }
        public async Task<IEnumerable<RecurringTransaction>> GetTransactionsByCategory(Guid userId, string categoryName)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecurringTransaction[]>($"/api/recurringtransactions/{userId}/filter/category={categoryName}");
            }
            catch (Exception ex)
            {
                return new List<RecurringTransaction>();
            }
        }
        public async Task<IEnumerable<RecurringTransaction>> Search(Guid userId, string query)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync <IEnumerable<RecurringTransaction>> ($"/api/recurringtransactions/{userId}/search={query}");
            }
            catch (Exception ex)
            {
                return new List<RecurringTransaction>();
            }
        }
        public async Task<bool> AddTransaction(NewRecurringTransaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PostAsync("/api/recurringtransactions", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateTransaction(RecurringTransaction transaction)
        {
            try
            {
                var content = await SerializeObj(transaction);
                var res = await _httpClient.PutAsync("/api/recurringtransactions", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> DeleteTransaction(Guid userId, Guid transactionId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/recurringtransactions/{userId}/{transactionId}");
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> IsRecurringTransactionDue(RecurringTransaction transaction)
        {
            try
            {
                DateTime now = DateTime.Now;

                if (now.Date > transaction.EndDate.Date)
                {
                    return false; // Transaction has ended
                }

                switch (transaction.Frequency)
                {
                    case "Daily":
                        return transaction.StartDate.Date <= now.Date;
                    case "Weekly":
                        return transaction.StartDate.Date <= now.Date &&
                               ((now - transaction.StartDate).Days % 7 == 0);
                    case "Monthly":
                        return transaction.StartDate.Day <= now.Day &&
                               ((now.Year - transaction.StartDate.Year) * 12 +
                                (now.Month - transaction.StartDate.Month)) % 1 == 0;
                    case "Quarterly":
                        int monthsDiff = (now.Year - transaction.StartDate.Year) * 12 +
                                         (now.Month - transaction.StartDate.Month);
                        return monthsDiff >= 0 && monthsDiff % 3 == 0 &&
                               now.Day >= Math.Min(transaction.StartDate.Day, DateTime.DaysInMonth(now.Year, now.Month));
                    case "Annually":
                        return now.DayOfYear ==
                               new DateTime(now.Year,
                                            transaction.StartDate.Month,
                                            Math.Min(transaction.StartDate.Day, DateTime.DaysInMonth(now.Year, transaction.StartDate.Month)))
                                    .DayOfYear;

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
