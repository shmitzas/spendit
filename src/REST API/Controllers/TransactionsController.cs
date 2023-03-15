using Microsoft.AspNetCore.Http.HttpResults;
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
                var transaction = await _DbContext.Transactions.Where(t => t.UserId == userId).OrderByDescending(t => t.CreatedAt).ToListAsync();
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
                var transactions = await _DbContext.Transactions.Where(t => t.Description.ToLower().Contains(query.ToLower()) && t.UserId == userId).ToListAsync();
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
                var transaction = new Transaction()
                {
                    Id = await _DbContext.Transactions.MaxAsync(t => (int)t.Id) + 1,
                    UserId = transactionInfo.UserId,
                    CategoryId = 0,
                    Type = transactionInfo.Type,
                    Amount = transactionInfo.Amount,
                    Currency = transactionInfo.Currency,
                    Description = transactionInfo.Description,
                    CreatedAt = transactionInfo.CreatedAt,
                    UpdatedAt = DateTime.Now
                };
                await _DbContext.Transactions.AddAsync(transaction);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(Transaction transactionInfo)
        {
            try
            {
                var transaction = await _DbContext.Transactions.Where(t => t.UserId == transactionInfo.UserId && t.Id == transactionInfo.Id).SingleAsync();
                transaction.CategoryId = transactionInfo.CategoryId;
                transaction.Type = transactionInfo.Type;
                transaction.Amount = transactionInfo.Amount;
                transaction.Currency = transactionInfo.Currency;
                transaction.Description = transactionInfo.Description;
                transaction.CreatedAt = transactionInfo.CreatedAt;
                transaction.UpdatedAt = DateTime.Now;

                _DbContext.Transactions.Update(transaction);
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
