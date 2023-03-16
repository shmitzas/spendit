using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API.Data;

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
    }
}
