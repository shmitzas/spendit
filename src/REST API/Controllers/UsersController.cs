using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using REST_API.Data;
using REST_API.Models.Users;
using System.Text.Json;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApiDbContext _DbContext;
        private readonly ILogger<UsersController> _logger;
        public UsersController(ApiDbContext DbContext, ILogger<UsersController> logger)
        {
            _DbContext = DbContext;
            _logger = logger;
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
            var User = await _DbContext.Users.Where(u => u.Id == id).SingleAsync();

            if (User != null)
            {
                return Ok(User);
            }
            return NotFound(new User());
        }
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetUserId(string username)
        {
            var User = await _DbContext.Users.Where(u => u.Username == username).SingleAsync();

            if (User != null)
            {
                return Ok(User);
            }
            return NotFound(new User());
        }

        //[HttpGet("auth/{credentials}")]
        //public async Task<IActionResult> SignUserIn(string credentials)
        //{
        //    try
        //    {
        //        var cred = credentials.Split(" ");
        //        var username = cred[0];
        //        var password = cred[1];

        //        var User = await _DbContext.Users.Where(u => u.Username == username && u.Password == password).SingleAsync();
        //        return Ok(User);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        //Console.WriteLine(ex.Message);
        //        return NotFound(new User());
        //    }
        //}

        [HttpPut("auth/")]
        public async Task<IActionResult> SignUserIn(User user)
        {
            try
            {
                var User = await _DbContext.Users.Where(u => u.Username == user.Username && u.Password == user.Password).SingleAsync();
                return Ok(User);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new User());
            }
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
        public async Task<IActionResult> UpdateUser(User userInfo)
        {
            try
            {
                var user = await _DbContext.Users.Where(u => u.Id == userInfo.Id).SingleAsync();
                if (userInfo.Password == null || userInfo.Email == null || userInfo.Settings == null)
                {
                    return NotFound();
                }
                user.Password = userInfo.Password;
                user.Email = userInfo.Email;
                user.Settings = userInfo.Settings;

                _DbContext.Users.Update(user);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
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
            return NotFound(new User());
        }
    }
}
