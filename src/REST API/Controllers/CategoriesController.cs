using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Categories;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public CategoriesController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _DbContext.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound("No categories found");
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            try
            {
                var category = await _DbContext.Categories.Where(t => t.Id == id).SingleAsync();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound("Category not found");
            }
        }
    }
}
