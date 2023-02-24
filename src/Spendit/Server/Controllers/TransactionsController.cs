using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spendit.Server.Data;
using Spendit.Server.Models.Categories;
using Spendit.Server.Models.Transactions;
using Microsoft.AspNetCore.Authorization;

namespace Spendit.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly API_DbContext _DbContext;
        public TransactionsController(API_DbContext DbContext) 
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("/get/{userId:int}")]
        async public Task<IActionResult> GetAllTransactions([FromRoute] int userId)
        {
            var result =  await _DbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
            return Ok(result);
        }


        [HttpGet]
        [Route("/find/{id:int}")]
        async public Task<IActionResult> GetTransaction([FromRoute] int id)
        {
            var Transaction = await _DbContext.Transactions.FindAsync(id);

            if (Transaction != null)
            {
                return Ok(Transaction);
            }
            return NotFound();
        }

        [HttpPost]
        async public Task<IActionResult> AddTransaction(NewTransaction TransactionInfo)
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
        [Route("{id:int}")]
        async public Task<IActionResult> UpdateTransaction([FromRoute] int id, UpdateTransaction TransactionInfo)
        {
            var Transaction = await _DbContext.Transactions.FindAsync(id);
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
        async public Task<IActionResult> DeleteTransaction([FromRoute] int userId, [FromRoute] int id)
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
