using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Categories;
using REST_API.Models.RecurringTransactions;
using REST_API.Models.Users;
using System;

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
                var rtransaction = await _DbContext.RTransactions.Where(t => t.UserId == userId).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(rtransaction);
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
                var rtransaction = await _DbContext.RTransactions.Where(t => t.UserId == userId && t.StartDate <= eDate && t.EndDate >= sDate).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(rtransaction);
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
                var rtransactions = await _DbContext.RTransactions.Where(t => t.Description.ToLower().Contains(query.ToLower()) && t.UserId == userId).OrderByDescending(t => t.StartDate).ToListAsync();
                return Ok(rtransactions);
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
                var rtransaction = await _DbContext.RTransactions
                    .Where(t => t.UserId == userId && t.Id == id)
                    .SingleAsync();
                return Ok(rtransaction);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(NewRTransaction rtransactionInfo)
        {
            try
            {
                var rtransaction = new RTransaction()
                {
                    Id = Guid.NewGuid(),
                    UserId = rtransactionInfo.UserId,
                    CategoryId = rtransactionInfo.CategoryId,
                    Type = rtransactionInfo.Type,
                    Amount = rtransactionInfo.Amount,
                    Currency = rtransactionInfo.Currency,
                    Description = rtransactionInfo.Description,
                    StartDate = rtransactionInfo.StartDate,
                    EndDate = rtransactionInfo.EndDate,
                    Frequency = rtransactionInfo.Frequency,
                };
                await _DbContext.RTransactions.AddAsync(rtransaction);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(RTransaction rtransactionInfo)
        {
            try
            {
                var rtransaction = await _DbContext.RTransactions.Where(t => t.UserId == rtransactionInfo.UserId && t.Id == rtransactionInfo.Id).SingleAsync();
                rtransaction.CategoryId = rtransactionInfo.CategoryId;
                rtransaction.Type = rtransactionInfo.Type;
                rtransaction.Amount = rtransactionInfo.Amount;
                rtransaction.Currency = rtransactionInfo.Currency;
                rtransaction.Description = rtransactionInfo.Description;
                rtransaction.StartDate = rtransactionInfo.StartDate;
                rtransaction.EndDate = rtransactionInfo.EndDate;
                rtransaction.Frequency = rtransactionInfo.Frequency;

                _DbContext.RTransactions.Update(rtransaction);
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
                var rtransaction = await _DbContext.RTransactions.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                _DbContext.Remove(rtransaction);
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
