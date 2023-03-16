using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Categories;
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
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
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
                Console.WriteLine(user.Username+ " " + userInfo.Password);
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
                    Id = Guid.NewGuid(),
                    Username = userInfo.Username,
                    Password = userInfo.Password,
                    Email = userInfo.Email,
                    Settings = "{\"currency\": \"EUR\"}"
                };

                var defaultCategory = new Category()
                {
                    Id = Guid.Empty,
                    UserId = user.Id,
                    Name = "Uncategorized"
                };
                await _DbContext.Users.AddAsync(user);
                await _DbContext.SaveChangesAsync();
                await _DbContext.Categories.AddAsync(defaultCategory);
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
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
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
