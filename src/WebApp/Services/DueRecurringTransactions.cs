using WebApp.Models.RecurringTransactions;
using WebApp.Models.Transactions;

namespace WebApp.Services
{
    public class DueRecurringTransactions
    {
        private readonly HttpClient _httpClient;
        public DueRecurringTransactions(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CycleTransactions(Guid userId)
        {
            try
            {
                var transactionsService = new TransactionsService(_httpClient);
                var recurringTransactionsService = new RecurringTransactionsService(_httpClient);
                var existingTransactions = await transactionsService.GetTransactions(userId);


                var recurringTransactions = await recurringTransactionsService.GetTransactions(userId);
                foreach (var recurringTransaction in recurringTransactions)
                {
                    if (await recurringTransactionsService.IsRecurringTransactionDue(recurringTransaction))
                    {
                        if (!existingTransactions.Any(t => 
                        t.Description.ToLower() == recurringTransaction.Description.ToLower() &&
                        t.CreatedAt.Date == DateTime.Now.Date
                        ))
                        {
                            var transaction = await ConvertToTransactionModel(recurringTransaction);
                            await transactionsService.AddTransaction(transaction);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private async Task<NewTransaction> ConvertToTransactionModel(RecurringTransaction recurringTransaction)
        {
            try
            {
                return new NewTransaction
                {
                    UserId = recurringTransaction.UserId,
                    CategoryId = recurringTransaction.CategoryId,
                    Type = recurringTransaction.Type,
                    Amount = recurringTransaction.Amount,
                    Currency = recurringTransaction.Currency,
                    Description = recurringTransaction.Description,
                    CreatedAt = DateTime.Now
                };
            }
            catch
            {
                return new NewTransaction();
            }
        }
    }
}
