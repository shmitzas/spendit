using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Users;
using System.Text.Json;

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
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _DbContext.Users.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var User = await _DbContext.Users.FindAsync(id);

            if (User != null)
            {
                return Ok(User);
            }
            return NotFound();
        }
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetUserId(string username)
        {
            var User = await _DbContext.Users.Where(u => u.Username == username).SingleAsync();

            if (User != null)
            {
                return Ok(User);
            }
            return NotFound();
        }

        [HttpGet("auth/{credentials}")]
        public async Task<IActionResult> SignUserIn(string credentials)
        {
            var cred = credentials.Split(" ");
            var username = cred[0];
            var password = cred[1];

            var User = await _DbContext.Users.Where(u => u.Username == username && u.Password == password).SingleAsync();

            if (User != null)
            {
                return Ok(User);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User NewUser)
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
        public async Task<IActionResult> UpdateUser(string content)
        {
            var UserInfo = JsonSerializer.Deserialize<User>(content);
            var User = await _DbContext.Users.Where(u => u.Id == UserInfo.Id).SingleAsync();
            
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
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
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
