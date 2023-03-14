using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Transactions;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public TransactionsController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetAllTransactions([FromRoute] int userId)
        {
            try
            {
                var transaction = await _DbContext.Transactions
                    .Where(t => t.UserId == userId)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:int}/search={query}")]
        public async Task<IActionResult> SearchTransactions([FromRoute] int userId, string query)
        {
            try
            {
                var transactions = await _DbContext.Transactions
                    .Where(t => t.Description.ToLower().Contains(query.ToLower()) && t.UserId == userId)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
                foreach(var tr in transactions)
                {
                    Console.WriteLine(tr.Description);
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:int}/{id:int}")]
        public async Task<IActionResult> GetTransaction([FromRoute] int userId, [FromRoute] int id)
        {
            try
            {
                var transaction = await _DbContext.Transactions
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
        public async Task<IActionResult> AddTransaction(Transaction transactionInfo)
        {
            try
            {
                var Transaction = new Transaction()
                {
                    UserId = transactionInfo.UserId,
                    CategoryId = 0,
                    Type = transactionInfo.Type,
                    Amount = transactionInfo.Amount,
                    Currency = transactionInfo.Currency,
                    Description = transactionInfo.Description,
                    CreatedAt = transactionInfo.CreatedAt == null ? DateTime.Now : transactionInfo.CreatedAt,
                    UpdatedAt = DateTime.Now
                };
                await _DbContext.Transactions.AddAsync(Transaction);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{userId:int}/{id:int}")]
        public async Task<IActionResult> UpdateTransaction([FromRoute] int userId, [FromRoute] int id, Transaction transactionInfo)
        {
            try
            {
                var Transaction = await _DbContext.Transactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                Transaction.CategoryId = transactionInfo.CategoryId;
                Transaction.Type = transactionInfo.Type;
                Transaction.Amount = transactionInfo.Amount;
                Transaction.Currency = transactionInfo.Currency;
                Transaction.Description = transactionInfo.Description;
                Transaction.UpdatedAt = DateTime.Now;

                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{userId:int}/{id:int}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int userId, [FromRoute] int id)
        {
            try
            {
                var transaction = await _DbContext.Transactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
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
