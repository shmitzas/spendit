using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spendit.Server.Data;
using Spendit.Server.Models.Users;

namespace Spendit.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly API_DbContext _DbContext;
        public UsersController(API_DbContext DbContext) 
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        async public Task<IActionResult> GetAllUsers()
        {
            var result = await _DbContext.Users.ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        async public Task<IActionResult> GetUser([FromRoute] int id)
        {
            var User = _DbContext.Users.FindAsync(id).Result;

            if (User != null)
            {
                return Ok(User);
            }
            return NotFound();
        }

        [HttpPost]
        async public Task<IActionResult> AddUser(NewUser NewUser)
        {
            var User = new User()
            {
                Username = NewUser.Username,
                Password = NewUser.Password,
                Email = NewUser.Email
            };
            await _DbContext.Users.AddAsync(User);
            await _DbContext.SaveChangesAsync();
            return Ok(User);
        }

        [HttpPut]
        [Route("{id:int}")]
        async public Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUser UserInfo)
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
        async public Task<IActionResult> DeleteUser([FromRoute] int id)
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
