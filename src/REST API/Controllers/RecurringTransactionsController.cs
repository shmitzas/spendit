using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.RecurringTransactions;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecurringTransactionsController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public RecurringTransactionsController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetAllTransactions([FromRoute] Guid userId)
        {
            try
            {
                var transaction = await _DbContext.RecurringTransactions.Where(t => t.UserId == userId).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:guid}/filter/start={startDate}&end={endDate}")]
        public async Task<IActionResult> GetTransactionsByDate([FromRoute] Guid userId, string startDate, string endDate)
        {
            var sDate = DateTime.Parse(startDate);
            var eDate = DateTime.Parse(endDate);
            try
            {
                var transaction = await _DbContext.RecurringTransactions.Where(t => t.UserId == userId && t.StartDate <= eDate && t.EndDate >= sDate).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:guid}/filter/category={category}")]
        public async Task<IActionResult> GetTransactionsByCategory([FromRoute] Guid userId, string category)
        {
            try
            {
                var categoryId = await _DbContext.Categories.Where(c => c.Name == category).SingleAsync();
                var transaction = await _DbContext.RecurringTransactions.Where(t => t.UserId == userId && t.CategoryId == categoryId.Id).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:guid}/filter/start={startDate}&end={endDate}/category={category}")]
        public async Task<IActionResult> GetTransactionsByDateAndCategory([FromRoute] Guid userId, string startDate, string endDate, string category)
        {
            try
            {
                var sDate = DateTime.Parse(startDate);
                var eDate = DateTime.Parse(endDate);
                var categoryId = await _DbContext.Categories.Where(c => c.Name == category).SingleAsync();
                var transaction = await _DbContext.RecurringTransactions.Where(t => t.UserId == userId && t.StartDate <= eDate && t.EndDate >= sDate && t.CategoryId == categoryId.Id).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:guid}/search={query}")]
        public async Task<IActionResult> SearchTransactions([FromRoute] Guid userId, string query)
        {
            try
            {
                var transactions = await _DbContext.RecurringTransactions.Where(t => t.Description.ToLower().Contains(query.ToLower()) && t.UserId == userId).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var transaction = await _DbContext.RecurringTransactions
                    .Where(t => t.UserId == userId && t.Id == id)
                    .SingleAsync();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddTransaction(NewRecurringTransaction transactionInfo)
        {
            try
            {
                var transaction = new RecurringTransaction()
                {
                    Id = Guid.NewGuid(),
                    UserId = transactionInfo.UserId,
                    CategoryId = transactionInfo.CategoryId,
                    Type = transactionInfo.Type,
                    Amount = transactionInfo.Amount,
                    Currency = transactionInfo.Currency,
                    Description = transactionInfo.Description,
                    StartDate = transactionInfo.StartDate,
                    EndDate = transactionInfo.EndDate,
                    Frequency = transactionInfo.Frequency,
                };
                await _DbContext.RecurringTransactions.AddAsync(transaction);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(RecurringTransaction transactionInfo)
        {
            try
            {
                var transaction = await _DbContext.RecurringTransactions.Where(t => t.UserId == transactionInfo.UserId && t.Id == transactionInfo.Id).SingleAsync();
                transaction.CategoryId = transactionInfo.CategoryId;
                transaction.Type = transactionInfo.Type;
                transaction.Amount = transactionInfo.Amount;
                transaction.Currency = transactionInfo.Currency;
                transaction.Description = transactionInfo.Description;
                transaction.StartDate = transactionInfo.StartDate;
                transaction.EndDate = transactionInfo.EndDate;
                transaction.Frequency = transactionInfo.Frequency;

                _DbContext.RecurringTransactions.Update(transaction);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var transaction = await _DbContext.RecurringTransactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                _DbContext.Remove(transaction);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
