using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Budgets;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public BudgetsController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetAllBudgets([FromRoute] Guid userId)
        {
            try
            {
                var budget = await _DbContext.Budgets.Where(t => t.UserId == userId).OrderBy(t => t.CreatedAt).ToListAsync();
                return Ok(budget);
            }
            catch
            {
                return NotFound("No Budgets found");
            }
        }
        [HttpGet]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> GetBudget([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var budget = await _DbContext.Budgets.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                return Ok(budget);
            }
            catch
            {
                return NotFound("Budget not found");
            }
        }
        [HttpGet]
        [Route("active/{userId:guid}")]
        public async Task<IActionResult> GetActiveBudget([FromRoute] Guid userId)
        {
            try
            {
                var budget = await _DbContext.Budgets.Where(t => t.UserId == userId && t.IsActive).SingleAsync();
                return Ok(budget);
            }
            catch
            {
                return NotFound("Budget not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget(NewBudget BudgetInfo)
        {
            try
            {
                var budget = new Budget()
                {
                    Id = Guid.NewGuid(),
                    UserId = BudgetInfo.UserId,
                    Description = BudgetInfo.Description,
                    Amount = BudgetInfo.Amount,
                    CurrentAmount = 0,
                    Currency = BudgetInfo.Currency,
                    IsActive = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _DbContext.Budgets.AddAsync(budget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Wrong Budget details");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBudget(Budget BudgetInfo)
        {
            try
            {
                var budget = await _DbContext.Budgets.Where(t => t.UserId == BudgetInfo.UserId && t.Id == BudgetInfo.Id).SingleAsync();
                budget.Description = BudgetInfo.Description;
                budget.Amount = BudgetInfo.Amount;
                budget.CurrentAmount = BudgetInfo.CurrentAmount;
                budget.Currency = BudgetInfo.Currency;
                budget.IsActive = BudgetInfo.IsActive;
                budget.CreatedAt = BudgetInfo.CreatedAt;
                budget.UpdatedAt = DateTime.Now;

                _DbContext.Budgets.Update(budget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound("Wrong Budget details");
            }
        }

        [HttpDelete]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> DeleteBudget([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var budget = await _DbContext.Budgets.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                
                var transactions = await _DbContext.Transactions.Where(t => t.BudgetId == id).ToListAsync();
                foreach (var transaction in transactions)
                {
                    transaction.BudgetId = Guid.Empty;
                    _DbContext.Transactions.Update(transaction);
                }
                _DbContext.Remove(budget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound("Budget does not exist");
            }
        }

        [HttpPut("activate")]
        public async Task<IActionResult> SetActiveBudget(Budget res)
        {
            try
            {
                var budgets = await _DbContext.Budgets.ToListAsync();
                foreach (var budget in budgets)
                {   
                    budget.IsActive = false;
                    _DbContext.Budgets.Update(budget);
                }
                var newActiveBudget = await _DbContext.Budgets.Where(t => t.Id == res.Id).SingleAsync();
                newActiveBudget.IsActive = true;
                _DbContext.Budgets.Update(newActiveBudget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Wrong Budget details");
            } 
        }
        [HttpPut("deactivate")]
        public async Task<IActionResult> SetInactiveBudget(Budget res)
        {
            try
            {
                var newInactiveBudget = await _DbContext.Budgets.Where(t => t.Id == res.Id).SingleAsync();
                newInactiveBudget.IsActive = false;
                _DbContext.Budgets.Update(newInactiveBudget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Wrong Budget details");
            }
        }
    }
}
