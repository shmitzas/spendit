using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Transactions;
using System;

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
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetAllTransactions([FromRoute] Guid userId)
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
        [Route("{userId:guid}/filter/start={startDate}&end={endDate}")]
        public async Task<IActionResult> GetTransactionsByDate([FromRoute] Guid userId, string startDate, string endDate)
        {
            var sDate = DateTime.Parse(startDate);
            var eDate = DateTime.Parse(endDate);
            eDate = eDate.AddDays(1).AddTicks(-1);
            try
            {
                var transaction = await _DbContext.Transactions.Where(t => t.UserId == userId && t.CreatedAt <= eDate && t.CreatedAt >= sDate).OrderByDescending(t => t.CreatedAt).ToListAsync();
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
                var transactions = await _DbContext.Transactions.Where(t => t.Description.ToLower().Contains(query.ToLower()) && t.UserId == userId).ToListAsync();
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
                    Id = transactionInfo.Id,
                    UserId = transactionInfo.UserId,
                    CategoryId = await GetCategoryId(transactionInfo.UserId), //Laikinas fixas kol neveikia kategorijos
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
                transaction.CategoryId = await GetCategoryId(transactionInfo.UserId);//Laikinas fixas kol neveikia kategorijos
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
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] Guid userId, [FromRoute] Guid id)
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
        //Laikinas fixas kol neveikia kategorijos
        private async Task<Guid> GetCategoryId(Guid userId)
        {
            try
            {
                var res = await _DbContext.Categories.Where(c => c.UserId == userId).SingleAsync();
                return res.Id;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
    }
}
