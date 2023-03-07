using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Users;

namespace REST_API.Controllers
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
            return Ok(_DbContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        async public Task<IActionResult> GetUser([FromRoute] int id)
        {
            var User = _DbContext.Users.FindAsync(id);

            if (User.Result != null)
            {
                return Ok(User.Result);
            }
            return NotFound();
        }
        [HttpGet("user/{username}")]
        async public Task<IActionResult> GetUserId(string username)
        {
            var User = _DbContext.Users.Where(u => u.Username == username).SingleAsync();

            if (User.Result != null)
            {
                return Ok(User.Result);
            }
            return NotFound();
        }

        [HttpGet("auth/{credentials}")]
        async public Task<IActionResult> SignUserIn(string credentials)
        {
            var cred = credentials.Split(" ");
            var username = cred[0];
            var password = cred[1];

            var User = _DbContext.Users.Where(u => u.Username == username && u.Password == password).SingleAsync();

            if (User.Result != null)
            {
                return Ok(User.Result);
            }
            return NotFound();
        }

        [HttpPost]
        async public Task<IActionResult> AddUser(User NewUser)
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
