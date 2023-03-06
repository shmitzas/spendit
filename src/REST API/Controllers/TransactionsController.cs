using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Users;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : Controller
    {
        private readonly API_DbContext _DbContext;
        public TransactionsController(API_DbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        async public Task<IActionResult> GetAllTransactions()
        {
            return Ok(_DbContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        async public Task<IActionResult> GetTransaction([FromRoute] int id)
        {
            var User = _DbContext.Users.FindAsync(id);

            if (User.Result != null)
            {
                return Ok(User);
            }
            return NotFound();
        }

        [HttpPost]
        async public Task<IActionResult> AddTransaction(User UserInfo)
        {
            var User = new User()
            {
                Username = UserInfo.Username,
                Password = UserInfo.Password,
                Email = UserInfo.Email,
                Settings = ""
            };
            await _DbContext.Users.AddAsync(User);
            await _DbContext.SaveChangesAsync();
            return Ok(User);
        }

        [HttpPut]
        [Route("{id:int}")]
        async public Task<IActionResult> UpdateTransaction([FromRoute] int id, UpdateUser UserInfo)
        {
            var User = _DbContext.Users.FindAsync(id).Result;

            if (User != null)
            {
                User.Password = UserInfo.Password;
                User.Email = UserInfo.Email;
                User.Settings = UserInfo.Settings;

                await _DbContext.SaveChangesAsync();
                return Ok(User);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        async public Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            var User = _DbContext.Users.FindAsync(id).Result;

            if (User != null)
            {
                _DbContext.Remove(User);

                await _DbContext.SaveChangesAsync();
                return Ok(User);
            }
            return NotFound();
        }
    }
}
