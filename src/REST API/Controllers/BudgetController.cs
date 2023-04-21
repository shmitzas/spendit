using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Budgets;

namespace REST_API.Controllers
{
    public class BudgetController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public BudgetController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetAllBudgets([FromRoute] Guid userId)
        {
            try
            {
                var budget = await _DbContext.Budgets.Where(t => t.UserId == userId).OrderByDescending(t => t.EndDate).ToListAsync();
                return Ok(budget);
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
                    StartDate = BudgetInfo.StartDate,
                    EndDate = BudgetInfo.EndDate,
                    Amount = BudgetInfo.Amount,
                    CurrentAmount = 0,
                    Currency = BudgetInfo.Currency,
                };
                await _DbContext.Budgets.AddAsync(budget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
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
                budget.StartDate = BudgetInfo.StartDate;
                budget.EndDate = BudgetInfo.EndDate;
                budget.Amount = BudgetInfo.Amount;
                budget.CurrentAmount = BudgetInfo.CurrentAmount;
                budget.Currency = BudgetInfo.Currency;

                _DbContext.Budgets.Update(budget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
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
                _DbContext.Remove(budget);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Budget does not exist");
            }
        }
    }
}
