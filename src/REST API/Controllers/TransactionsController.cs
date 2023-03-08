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
        private readonly API_DbContext _DbContext;
        public TransactionsController(API_DbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetAllTransactions([FromRoute] int userId)
        {
            var result = await _DbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
            return Ok(result);
        }


        [HttpGet]
        [Route("{userId:int}/{id:int}")]
        public async Task<IActionResult> GetTransaction([FromRoute] int userId, [FromRoute] int id)
        {
            var Transaction = await _DbContext.Transactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();

            if (Transaction != null)
            {
                return Ok(Transaction);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(Transaction TransactionInfo)
        {
            var Transaction = new Transaction()
            {
                UserId = TransactionInfo.UserId,
                CategoryId = TransactionInfo.CategoryId,
                Type = TransactionInfo.Type,
                Amount = TransactionInfo.Amount,
                Currency = TransactionInfo.Currency,
                Description = TransactionInfo.Description
            };
            await _DbContext.Transactions.AddAsync(Transaction);
            await _DbContext.SaveChangesAsync();
            return Ok(Transaction);
        }

        [HttpPut]
        [Route("{userId:int}/{id:int}")]
        public async Task<IActionResult> UpdateTransaction([FromRoute] int userId, [FromRoute] int id, Transaction TransactionInfo)
        {
            var Transaction = await _DbContext.Transactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
            if (Transaction != null)
            {
                Transaction.CategoryId = TransactionInfo.CategoryId;
                Transaction.Type = TransactionInfo.Type;
                Transaction.Amount = TransactionInfo.Amount;
                Transaction.Currency = TransactionInfo.Currency;
                Transaction.Description = TransactionInfo.Description;

                await _DbContext.SaveChangesAsync();
                return Ok(Transaction);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{userId:int}/{id:int}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int userId, [FromRoute] int id)
        {
            var Transaction = await _DbContext.Transactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();

            if (Transaction != null)
            {
                _DbContext.Remove(Transaction);

                await _DbContext.SaveChangesAsync();
                return Ok(Transaction);
            }
            return NotFound();
        }
    }
}
