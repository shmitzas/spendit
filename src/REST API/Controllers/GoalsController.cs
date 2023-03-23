﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Goals;
using REST_API.Models.Users;
using System;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public GoalsController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetAllGoals([FromRoute] Guid userId)
        {
            try
            {
                var goal = await _DbContext.Goals.Where(t => t.UserId == userId).OrderByDescending(t => t.EndDate).ToListAsync();
                return Ok(goal);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> GetGoal([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var goal = await _DbContext.Goals.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                return Ok(goal);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddGoal(NewGoal goalInfo)
        {
            try
            {
                var goal = new Goal()
                {
                    Id = Guid.NewGuid(),
                    UserId = goalInfo.UserId,
                    CategoryId = goalInfo.CategoryId,
                    Description = goalInfo.Description,
                    StartDate = DateTime.Now,
                    EndDate = goalInfo.EndDate,
                    Amount = goalInfo.Amount,
                    CurrentAmount = 0,
                    Currency = goalInfo.Currency,
                };
                await _DbContext.Goals.AddAsync(goal);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGoal(Goal goalInfo)
        {
            try
            {
                var goal = await _DbContext.Goals.Where(t => t.UserId == goalInfo.UserId && t.Id == goalInfo.Id).SingleAsync();
                goal.Description = goalInfo.Description;
                goal.StartDate = goalInfo.StartDate;
                goal.EndDate = goalInfo.EndDate;
                goal.Amount = goalInfo.Amount;
                goal.CurrentAmount = goalInfo.CurrentAmount;
                goal.Currency = goalInfo.Currency;

                _DbContext.Goals.Update(goal);
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
        public async Task<IActionResult> DeleteGoal([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var goal = await _DbContext.Goals.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                _DbContext.Remove(goal);
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
