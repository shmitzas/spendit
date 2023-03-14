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
        private readonly ApiDbContext _DbContext;
        public UsersController(ApiDbContext DbContext)
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
            try
            {
                var user = await _DbContext.Users.Where(u => u.Id == id).SingleAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut("auth")]
        public async Task<IActionResult> SignUserIn(User userInfo)
        {
            try
            {
                var user = await _DbContext.Users.Where(u => u.Username == userInfo.Username && u.Password == userInfo.Password).SingleAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User userInfo)
        {
            try
            {
                var user = new User()
                {
                    Id = await _DbContext.Users.MaxAsync(u => (int)u.Id)+1,
                    Username = userInfo.Username,
                    Password = userInfo.Password,
                    Email = userInfo.Email,
                    Settings = "{\"currency\": \"EUR\"}"
                };
                await _DbContext.Users.AddAsync(user);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
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
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var user = await _DbContext.Users.Where(u => u.Id == id).SingleAsync();
                _DbContext.Remove(user);
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
